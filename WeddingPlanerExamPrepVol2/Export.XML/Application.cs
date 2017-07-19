
namespace Export.XML
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using WeddingPlanner.Data;

    public class Application
    {
        public static void Main()
        {
            ExportVenuesInSofia();
        }

        private static void ExportVenuesInSofia()
        {
            using (WeddingContext context = new WeddingContext())
            {
                var venues = context.Venues.Where(v => v.Town == "Sofia" && v.Weddings.Count < 3).ToList();
                XDocument xmlDoc = new XDocument();
                XElement venuesXml = new XElement("venues");
                venuesXml.SetAttributeValue("town", "Sofia");

                foreach (var v in venues)
                {
                    XElement venueXml = new XElement("venue");
                    venueXml.SetAttributeValue("name", v.Name);
                    venueXml.SetAttributeValue("capacity", v.Capacity);

                    XElement weddingXml = new XElement("weddings-count");
                    weddingXml.Value = v.Weddings.Count().ToString();

                    venueXml.Add(weddingXml);
                    venuesXml.Add(venueXml);
                }
                xmlDoc.Add(venuesXml);
                xmlDoc.Save("../../../Export/venuesInSofia.xml");
            }
        }
    }
}
