
namespace Export.XML
{
    using SolarSystem.Data;
    using System.Linq;
    using System.Xml.Linq;

    public class App
    {
        public static void Main()
        {
            using (SolarSystemContext context = new SolarSystemContext())
            {
                var anomalies = context.Anomalies
                    .Select(a => new
                    {
                        id = a.Id,
                        originPlanet = a.OriginPlanet.Name,
                        teleportPlanet = a.TeleportPlanet.Name,
                        victims = a.Victims.Select(v => new
                        {
                            name = v.Name
                        })
                    });

                XDocument xmlDoc = new XDocument();
                XElement anomaliesXml = new XElement("anomalies");

                foreach (var anomaly in anomalies)
                {
                    XElement anomalyXml = new XElement("anomaly");
                    anomalyXml.SetAttributeValue("id", anomaly.id);
                    anomalyXml.SetAttributeValue("origin-planet", anomaly.originPlanet);
                    anomalyXml.SetAttributeValue("teleport-planet", anomaly.teleportPlanet);

                    XElement victimsXml = new XElement("victims");

                    foreach (var vic in anomaly.victims)
                    {
                        XElement victimXml = new XElement("victim");
                        victimXml.SetAttributeValue("name", vic.name);

                        victimsXml.Add(victimXml);
                    }
                    anomalyXml.Add(victimsXml);
                    anomaliesXml.Add(anomalyXml);
                }
                xmlDoc.Add(anomaliesXml);
                xmlDoc.Save("../../../Export/victims.xml");
            }
        }
    }
}
