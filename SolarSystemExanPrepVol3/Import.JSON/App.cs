
namespace Import.JSON
{
    using DTO;
    using Newtonsoft.Json;
    using SolarSystem.Data;
    using SolarSystem.Models;
    using SolarSystem.Utility;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class App
    {
        public static void Main()
        {
            //ImportSolarSystems();
            //ImportingStars();
            //ImportPlanets();
            //ImportPeople();
            //ImportAnomalies();
            //ImportAnomalyVictims();

        }

        private static void ImportAnomalyVictims()
        {
            string json = File.ReadAllText(Constants.AnomalyVictimsPath);
            List<AnomalyVictimDto> anomalyVictimsDto = JsonConvert.DeserializeObject<List<AnomalyVictimDto>>(json);

            using (SolarSystemContext context = new SolarSystemContext())
            {
                foreach (var a in anomalyVictimsDto)
                {
                    if (a.Id <= 0 || a.Person == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Anomaly anomaly = context.Anomalies.FirstOrDefault(an => an.Id == a.Id);
                    Person person = context.People.FirstOrDefault(p => p.Name == a.Person);

                    if (anomaly == null || person == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    anomaly.Victims.Add(person);
                    context.SaveChanges();
                    Console.WriteLine($"Succesfuly added victim to an anomaly");
                }
            }
        }

        private static void ImportAnomalies()
        {
            string json = File.ReadAllText(Constants.AnomaliesPath);
            List<AnomalyDto> anomaliesDto = JsonConvert.DeserializeObject<List<AnomalyDto>>(json);

            using (SolarSystemContext context = new SolarSystemContext())
            {
                foreach (var a in anomaliesDto)
                {
                    if (a.OriginPlanet == null || a.TeleportPlanet == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Planet originPlanet = context.Planets.FirstOrDefault(p => p.Name == a.OriginPlanet);
                    Planet teleportPlanet = context.Planets.FirstOrDefault(p => p.Name == a.TeleportPlanet);

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

                    try
                    {
                        context.Anomalies.Add(anomaly);
                        context.SaveChanges();
                        Console.WriteLine($"Succesfully added anomly with origin planet {a.OriginPlanet} and teleport planet {a.TeleportPlanet}");
                    }
                    catch (Exception)
                    {
                        context.Anomalies.Remove(anomaly);
                        Console.WriteLine(Messages.ErrorMessage);
                    }
                }
            }
        }

        private static void ImportPeople()
        {
            string json = File.ReadAllText(Constants.PeoplePath);
            List<PersonDto> peopleDto = JsonConvert.DeserializeObject<List<PersonDto>>(json);

            using (SolarSystemContext context = new SolarSystemContext())
            {
                foreach (var p in peopleDto)
                {
                    if (p.Name == null || p.HomePlanet == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Planet homePlanet = context.Planets.FirstOrDefault(pl => pl.Name == p.HomePlanet);

                    if (homePlanet == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Person person = new Person
                    {
                        Name = p.Name,
                        HomePlanet = homePlanet
                    };

                    try
                    {
                        context.People.Add(person);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully added person {p.Name} with homeplanet {p.HomePlanet}");
                    }
                    catch (Exception)
                    {
                        context.People.Remove(person);
                        Console.WriteLine(Messages.ErrorMessage);
                    }
                }
            }
        }

        private static void ImportPlanets()
        {
            string json = File.ReadAllText(Constants.PlanetsPath);
            List<PlanetDto> planetsDto = JsonConvert.DeserializeObject<List<PlanetDto>>(json);

            using (SolarSystemContext context = new SolarSystemContext())
            {
                foreach (var p in planetsDto)
                {
                    if (p.Name == null || p.SolarSystem == null || p.Sun == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    SolarSystem solarSystem = context.SolarSystems.FirstOrDefault(s => s.Name == p.SolarSystem);
                    Star sun = context.Stars.FirstOrDefault(s => s.Name == p.Sun);

                    if (solarSystem == null || sun == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Planet planet = new Planet
                    {
                        Name = p.Name,
                        SolarSystem = solarSystem,
                        Sun = sun
                    };

                    try
                    {
                        context.Planets.Add(planet);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully added planet {p.Name}");
                    }
                    catch (Exception)
                    {
                        context.Planets.Remove(planet);
                        Console.WriteLine(Messages.ErrorMessage);
                    }
                }
            }
        }

        private static void ImportingStars()
        {
            string json = File.ReadAllText(Constants.StarsPath);
            List<StarDto> starsDto = JsonConvert.DeserializeObject<List<StarDto>>(json);

            using (SolarSystemContext context = new SolarSystemContext())
            {
                foreach (var star in starsDto)
                {
                    if (star.Name == null || star.SolarSystem == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    SolarSystem solarSystem = context.SolarSystems.FirstOrDefault(s => s.Name == star.SolarSystem);

                    if (solarSystem == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    Star starr = new Star
                    {
                        Name = star.Name,
                        SolarSystem = solarSystem
                    };

                    try
                    {
                        context.Stars.Add(starr);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported Star {starr.Name}");
                    }
                    catch (Exception)
                    {
                        context.Stars.Remove(starr);
                        Console.WriteLine(Messages.ErrorMessage);
                    }

                }
            }
        }

        private static void ImportSolarSystems()
        {
            string json = File.ReadAllText(Constants.SolarSystemsPath);
            List<SolarSystemDto> solarSystems = JsonConvert.DeserializeObject<List<SolarSystemDto>>(json);

            using (SolarSystemContext context = new SolarSystemContext())
            {
                foreach (var ss in solarSystems)
                {
                    if (ss.Name == null)
                    {
                        Console.WriteLine(Messages.ErrorMessage);
                        continue;
                    }

                    SolarSystem solarSystem = new SolarSystem
                    {
                        Name = ss.Name
                    };

                    try
                    {
                        context.SolarSystems.Add(solarSystem);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported Solar System {solarSystem.Name}");
                    }
                    catch (Exception)
                    {
                        context.SolarSystems.Remove(solarSystem);
                        Console.WriteLine(Messages.ErrorMessage);
                    }

                }
            }
        }
    }
}
