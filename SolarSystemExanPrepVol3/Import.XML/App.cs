
namespace Import.XML
{
    using SolarSystem.Data;
    using SolarSystem.Utility;
    using System.Xml.Linq;
    using System;
    using SolarSystem.Models;
    using System.Linq;
    using System.Xml.XPath;

    public class App
    {
        public static void Main()
        {
            XDocument xmlDoc = XDocument.Load(Constants.NewAnomaliesPath);
            var anomaliesXml = xmlDoc.Root.Elements();

            using (SolarSystemContext context = new SolarSystemContext())
            {
                foreach (var a in anomaliesXml)
                {
                    string originPlanetName = a.Attribute("origin-planet")?.Value;
                    string teleportPlanetName = a.Attribute("teleport-planet")?.Value;

                    if (originPlanetName == null || teleportPlanetName == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Planet originPlanet = context.Planets.FirstOrDefault(p => p.Name == originPlanetName);
                    Planet teleportPlanet = context.Planets.FirstOrDefault(p => p.Name == teleportPlanetName);

                    if (originPlanet == null || teleportPlanet == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Anomaly anomaly = new Anomaly
                    {
                        OriginPlanet = originPlanet,
                        TeleportPlanet = teleportPlanet
                    };

                    var victims = a.XPathSelectElements("victims/victim");

                    foreach (var vic in victims)
                    {
                        string personName = vic.Attribute("name")?.Value;
                        if (personName == null)
                        {
                            continue;
                        }
                        Person person = context.People.FirstOrDefault(p => p.Name == personName);

                        if (person == null)
                        {
                            continue;
                        }
                        anomaly.Victims.Add(person);
                    }

                    context.Anomalies.Add(anomaly);
                    context.SaveChanges();
                    Console.WriteLine("Successfully imported anomaly.");
                }
            }
        }
    }
}
