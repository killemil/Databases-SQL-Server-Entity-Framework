
namespace Export.JSON
{
    using Newtonsoft.Json;
    using PhotographyWorkshop.Data;
    using PhotographyWorkshop.Models;
    using System;
    using System.IO;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            //ExportOrderedPhotographers();
            //ExportLandscapePhotographers();
        }

        private static void ExportLandscapePhotographers()
        {
            using (PhotographyContext context = new PhotographyContext())
            {
                var photographers = context.Photographers
                    .Where(p => p.PrimaryCamera is DslrCamera
                    && p.Lenses.All(l => l.FocalLength <= 30)
                    && p.Lenses.Count > 0)
                    .OrderBy(p => p.FirstName)
                    .Select(p => new
                    {
                        FirsName = p.FirstName,
                        LastName = p.LastName,
                        CameraMake = p.PrimaryCamera.Make,
                        LensesCount = p.Lenses.Count()
                    });

                string json = JsonConvert.SerializeObject(photographers, Formatting.Indented);
                Console.WriteLine(json);

                File.WriteAllText("../../../landscape-photographers.json", json);
            }
        }

        private static void ExportOrderedPhotographers()
        {
            using (PhotographyContext context = new PhotographyContext())
            {
                var photographers = context.Photographers
                    .OrderBy(p => p.FirstName)
                    .ThenByDescending(p => p.LastName)
                    .Select(p => new
                    {
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Phone = p.PhoneNumber
                    });

                string json = JsonConvert.SerializeObject(photographers, Formatting.Indented);
                Console.WriteLine(json);

                File.WriteAllText("../../../photographers-ordered.json", json);
            }
        }
    }
}
