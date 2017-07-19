namespace Import.XML
{
    using PhotographyWorkshop.Models;
    using PhotographyWorkshop.Utility;
    using System.Xml.Linq;
    using System;
    using PhotographyWorkshop.Data;
    using System.Linq;
    using System.Xml.XPath;

    public class ImportXml
    {

        public static void Main()
        {
            //ImportAccessories();
            ImportWorkshops();
        }

        private static void ImportWorkshops()
        {
            XDocument xmlDoc = XDocument.Load(Constants.WorkshopsPath);
            var workshopsXml = xmlDoc.XPathSelectElements("workshops/workshop");
            using (PhotographyContext context = new PhotographyContext())
            {
                foreach (var w in workshopsXml)
                {
                    var name = GetAttribute(w, "name");
                    var location = GetAttribute(w, "location");
                    var price = GetAttribute(w, "price");
                    var trainerName = w.XPathSelectElement("trainer");


                    if (name == null || location == null || price == null || trainerName == null)
                    {
                        Console.WriteLine(Messages.Error);
                        continue;
                    }

                    DateTime? startDate = GetDateOrNull(w, "start-date");
                    DateTime? endDate = GetDateOrNull(w, "end-date");

                    var trainer = context.Photographers
                        .Where(p => p.FirstName + " " + p.LastName == trainerName.Value)
                        .FirstOrDefault();

                    Workshop workshop = new Workshop
                    {
                        Name = name.Value,
                        StartDate = startDate,
                        EndDate = endDate,
                        Location = location.Value,
                        PricePerParticipant = Convert.ToDecimal(price.Value),
                        Trainer = trainer
                    };

                    var participants = w.XPathSelectElements("participants/participant");

                    foreach (var p in participants)
                    {
                        string firstName = p.Attribute("first-name").Value;
                        string lastName = p.Attribute("last-name").Value;

                        var participant = context.Photographers
                            .Where(ph => ph.FirstName == firstName && ph.LastName == lastName)
                            .FirstOrDefault();
                        if (participant != null)
                        {
                            workshop.Participants.Add(participant);
                        }
                    }
                    context.Workshops.Add(workshop);
                    context.SaveChanges();
                    Console.WriteLine($"Successfully imported {workshop.Name}");
                }

            }

        }

        private static XAttribute GetAttribute(XElement element, string attributeName)
        {
            return element.Attributes().FirstOrDefault(a => a.Name == attributeName);
        }

        private static DateTime? GetDateOrNull(XElement element, string attributeName)
        {
            DateTime? date = null;
            if (GetAttribute(element, attributeName) != null)
            {
                date = DateTime.Parse(element.Attribute(attributeName).Value);
            }
            return date;
        }

        private static void ImportAccessories()
        {
            XDocument xmlDoc = XDocument.Load(Constants.AccessoriesPath);
            var accessoryXml = xmlDoc.Root.Elements();

            using (PhotographyContext context = new PhotographyContext())
            {

                foreach (var a in accessoryXml)
                {
                    if (a.Attribute("name").Value == null)
                    {
                        Console.WriteLine(Messages.Error);
                        continue;
                    }
                    Accessory accessory = new Accessory
                    {
                        Name = a.Attribute("name").Value,
                        Owner = GetRandomOwner(context)
                    };
                    context.Accessories.Add(accessory);
                    context.SaveChanges();

                    Console.WriteLine($"Successfully imported {accessory.Name}");
                }
            }
        }

        private static Photographer GetRandomOwner(PhotographyContext context)
        {
            Random random = new Random();
            int randomId = random.Next(1, context.Photographers.Count());

            return context.Photographers.Find(randomId);
        }
    }
}

