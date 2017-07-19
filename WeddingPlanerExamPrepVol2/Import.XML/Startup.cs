
namespace Import.XML
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using WeddingPlanner.Data;
    using WeddingPlanner.Models;
    using WeddingPlanner.Models.Enums;
    using WeddingPlanner.Utilities;

    public class Startup
    {
        public static void Main()
        {
            //ImportVenues();
            ImportPresents();
        }

        private static void ImportPresents()
        {
            XDocument xmlDoc = XDocument.Load(Constants.PresentsPath);
            var presentsXml = xmlDoc.Root.Elements();

            using (WeddingContext context = new WeddingContext())
            {
                foreach (var p in presentsXml)
                {
                    string type = p.Attribute("type")?.Value;
                    int invitationId = Convert.ToInt32(p.Attribute("invitation-id")?.Value);

                    if (type == null || invitationId == 0)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    if (type == "cash")
                    {
                        decimal cash = Convert.ToDecimal(p.Attribute("amount")?.Value);
                        Invitation invitation = context.Invitations.FirstOrDefault(i => i.Id == invitationId);

                        if (invitation == null || cash == 0)
                        {
                            Console.WriteLine(Messages.ErrorMessage);
                            continue;
                        }

                        Cash cashPresent = new Cash
                        {
                            CashAmount = cash,
                            Invitation = invitation
                        };
                        context.Presents.Add(cashPresent);
                        Console.WriteLine($"Succesfully imported gift from {invitation.Guest.FullName}");

                    }

                    if (type == "gift")
                    {
                        Invitation invitation = context.Invitations.FirstOrDefault(i => i.Id == invitationId);
                        string presentName = p.Attribute("present-name")?.Value;

                        string pSize = p.Attribute("size")?.Value;
                        PresentSize size;
                        bool IsPresentSizeValid = Enum.TryParse(pSize, true, out size);

                        if (pSize == null)
                        {
                            size = PresentSize.NotSpecified;
                        }
                        if (!IsPresentSizeValid)
                        {
                            Console.WriteLine(Messages.ErrorMessage);
                            continue;
                        }

                        if (invitation == null || presentName == null)
                        {
                            Console.WriteLine(Messages.ErrorMessage);
                            continue;
                        }

                        Gift giftPresent = new Gift
                        {
                            Invitation = invitation,
                            Name = presentName,
                            Size = size
                        };
                        context.Presents.Add(giftPresent);
                        Console.WriteLine($"Succesfully imported gift from {invitation.Guest.FullName}");
                    }
                    context.SaveChanges();

                }
            }
        }

        private static void ImportVenues()
        {
            XDocument xmlDoc = XDocument.Load(Constants.VenuesPath);
            var venuesXml = xmlDoc.Root.Elements();

            using (WeddingContext context = new WeddingContext())
            {
                foreach (var v in venuesXml)
                {
                    string name = v.Attribute("name").Value;
                    int capacity = Convert.ToInt32(v.Element("capacity").Value);
                    string town = v.Element("town").Value;

                    Venue venue = new Venue
                    {
                        Name = name,
                        Capacity = capacity,
                        Town = town
                    };
                    Random random = new Random();
                    var randomId = random.Next(1, context.Weddings.Count() - 1);
                    var wedding1 = context.Weddings.Find(randomId);
                    var wedding2 = context.Weddings.Find(randomId + 1);

                    venue.Weddings.Add(wedding1);
                    venue.Weddings.Add(wedding2);

                    try
                    {
                        context.Venues.Add(venue);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported {venue.Name}");
                    }
                    catch (Exception)
                    {
                        context.Venues.Remove(venue);
                        Console.WriteLine(Messages.ErrorMessage);
                    }
                }
            }
        }
    }
}
