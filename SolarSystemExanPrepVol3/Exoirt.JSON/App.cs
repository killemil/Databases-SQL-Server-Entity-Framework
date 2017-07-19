
namespace Exoirt.JSON
{
    using Newtonsoft.Json;
    using SolarSystem.Data;
    using System.IO;
    using System.Linq;

    public class App
    {
        public static void Main()
        {
            //ExoportPlanetsWichAreNotAnomalyOrigins();
            //ExportPeopleWhichAreNotVictims();
            MostAffectedAnomaly();
        }

        private static void MostAffectedAnomaly()
        {
            using (SolarSystemContext context = new SolarSystemContext())
            {
                var anomaly = context.Anomalies
                    .OrderByDescending(a => a.Victims.Count())
                    .Take(1)
                    .Select(a => new
                    {
                        id = a.Id,
                        originPlanet = new
                        {
                            name = a.OriginPlanet.Name
                        },
                        teleportPlanet = new
                        {
                            name = a.TeleportPlanet.Name
                        },
                        victimesCount = a.Victims.Count()
                    });

                string json = JsonConvert.SerializeObject(anomaly, Formatting.Indented);
                File.WriteAllText("../../../Export/topAnomaly.json", json);
            }
        }

        private static void ExportPeopleWhichAreNotVictims()
        {
            using (SolarSystemContext context = new SolarSystemContext())
            {
                var nonVictims = context.People
                    .Where(p => p.Anomalies.Count == 0)
                    .Select(p => new
                    {
                        name = p.Name,
                        homeplanet = new
                        {
                            name = p.HomePlanet.Name
                        }
                    });

                string json = JsonConvert.SerializeObject(nonVictims, Formatting.Indented);
                File.WriteAllText("../../../Export/nonvictims.json", json);

            }
        }

        private static void ExoportPlanetsWichAreNotAnomalyOrigins()
        {
            using (SolarSystemContext context = new SolarSystemContext())
            {
                var planets = context.Planets
                    .Where(p => p.OriginAnomalies.Count == 0)
                    .Select(p => new
                    {
                        name = p.Name
                    });

                string json = JsonConvert.SerializeObject(planets, Formatting.Indented);

                File.WriteAllText("../../../Export/planetswithoutanomalies.json", json);
            }
        }
    }
}
