namespace Import.JSON
{
    using Newtonsoft.Json;
    using PhotographyWorkshop.Data;
    using PhotographyWorkshop.Data.DTO;
    using PhotographyWorkshop.Models;
    using PhotographyWorkshop.Utility;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ImportJson
    {
        public static void Main()
        {
            ImportLenses();
            ImportCameras();
            ImportPhotographers();
        }

        private static Camera GetRandomCamera(PhotographyContext context)
        {

            Random random = new Random();
            int randomId = random.Next(1, context.Cameras.Count());

            return context.Cameras.Find(randomId);

        }
        private static void ImportPhotographers()
        {
            string json = File.ReadAllText(Constants.PhotographersPath);
            List<PhotographerDTO> photographersDto = JsonConvert.DeserializeObject<List<PhotographerDTO>>(json);

            using (PhotographyContext context = new PhotographyContext())
            {


                foreach (var p in photographersDto)
                {
                    if (p.FirstName == null || p.LastName == null)
                    {
                        Console.WriteLine(Messages.Error);
                        continue;
                    }
                    Photographer photographer = new Photographer
                    {
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        PhoneNumber = p.Phone,
                        PrimaryCamera = GetRandomCamera(context),
                        SecondaryCamera = GetRandomCamera(context)
                    };

                    foreach (var lensId in p.Lenses)
                    {
                        Lens lens = context.Lenses.FirstOrDefault(l => l.Id == lensId);

                        if (lens != null)
                        {
                            if (CheckLensCompatibility(lens, photographer))
                            {
                                photographer.Lenses.Add(lens);
                            }
                        }
                    }

                    try
                    {
                        context.Photographers.Add(photographer);
                        context.SaveChanges();
                        Console.WriteLine($"Successfully imported {photographer.FirstName} {photographer.LastName} | Lenses: {photographer.Lenses.Count}");
                    }
                    catch (Exception)
                    {
                        context.Photographers.Remove(photographer);
                        Console.WriteLine(Messages.Error);
                    }
                }


            }

        }

        private static bool CheckLensCompatibility(Lens lens, Photographer photographer)
        {
            if (photographer.PrimaryCamera.Make == lens.CompatibleWith
                || photographer.SecondaryCamera.Make == lens.CompatibleWith)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static void ImportCameras()
        {
            string json = File.ReadAllText(Constants.CamerasPath);

            List<CameraDTO> camerasDto = JsonConvert.DeserializeObject<List<CameraDTO>>(json);

            foreach (var c in camerasDto)
            {
                using (PhotographyContext context = new PhotographyContext())
                {
                    if (c.Make == null
                        || c.Model == null
                        || c.Type == null
                        || c.minISO <= 0)
                    {
                        Console.WriteLine(Messages.Error);
                        continue;
                    }

                    if (c.Type == "Mirrorless")
                    {
                        MirrorlessCamera mirrorlessCamera = new MirrorlessCamera
                        {
                            Make = c.Make,
                            Model = c.Model,
                            MinISO = c.minISO,
                            MaxISO = c.MaxISO,
                            MaxVideoResolution = c.MaxVideoResolution,
                            MaxFrameRate = c.MaxFrame,
                            IsFullFrame = c.IsFullFrame
                        };
                        context.Cameras.Add(mirrorlessCamera);
                        Console.WriteLine($"Successfully imported Mirrorless {c.Make} {c.Model}");
                    }

                    if (c.Type == "DSLR")
                    {
                        DslrCamera dslrCamera = new DslrCamera
                        {
                            Make = c.Make,
                            Model = c.Model,
                            MinISO = c.minISO,
                            MaxISO = c.MaxISO,
                            IsFullFrame = c.IsFullFrame,
                            MaxShutterSpeed = c.MaxShutterSpeed,
                        };
                        context.Cameras.Add(dslrCamera);
                        Console.WriteLine($"Successfully imported DSLR {c.Make} {c.Model}");
                    }

                    context.SaveChanges();
                }
            }
        }

        private static void ImportLenses()
        {
            string json = File.ReadAllText(Constants.LensPath);
            List<LensDTO> lensesDto = JsonConvert.DeserializeObject<List<LensDTO>>(json);

            List<Lens> lenses = new List<Lens>();
            foreach (var l in lensesDto)
            {
                if (l.Make == null
                    || l.focalLength == null
                    || l.maxAperture == null
                    || l.CompatibleWith == null)
                {
                    Console.WriteLine(Messages.Error);
                    continue;
                }
                Lens lens = new Lens
                {
                    Make = l.Make,
                    FocalLength = Convert.ToInt32(l.focalLength),
                    MaxAperture = float.Parse(l.maxAperture),
                    CompatibleWith = l.CompatibleWith
                };
                lenses.Add(lens);
                Console.WriteLine($"Succesfully imported {l.Make} {l.focalLength}mm f{l.maxAperture}");
            }

            using (PhotographyContext context = new PhotographyContext())
            {
                context.Lenses.AddRange(lenses);
                context.SaveChanges();
            }
        }
    }
}
