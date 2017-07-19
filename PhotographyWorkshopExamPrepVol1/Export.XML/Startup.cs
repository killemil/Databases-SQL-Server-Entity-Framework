
namespace Export.XML
{
    using PhotographyWorkshop.Data;
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class Startup
    {
        public static void Main()
        {
            //ExportPhotographersWithSameCameraMake();
            ExportWorkshopsByLocation();
        }

        private static void ExportWorkshopsByLocation()
        {

        }

        private static void ExportPhotographersWithSameCameraMake()
        {
            using (PhotographyContext context = new PhotographyContext())
            {
                var photographers = context.Photographers
                    .Where(p => p.PrimaryCamera.Make == p.SecondaryCamera.Make && p.Lenses.Count > 0)
                    .Select(p => new
                    {
                        FullName = p.FirstName + " " + p.LastName,
                        PrimaryCamera = p.PrimaryCamera.Make + " " + p.PrimaryCamera.Model,
                        Lenses = p.Lenses.Select(l => new
                        {
                            Make = l.Make,
                            FocalLength = l.FocalLength + "mm",
                            MaxAperture = "f" + l.MaxAperture
                        })
                    });

                XDocument xmlDoc = new XDocument();
                XElement photographersXml = new XElement("photographers");


                foreach (var p in photographers)
                {
                    XElement photographer = new XElement("photographer");
                    photographer.SetAttributeValue("name", p.FullName);
                    photographer.SetAttributeValue("primary-camera", p.PrimaryCamera);

                    XElement lenses = new XElement("lenses");
                    foreach (var l in p.Lenses)
                    {
                        XElement lens = new XElement("lens");
                        lens.Value = l.Make + " " + l.FocalLength + " " + l.MaxAperture;

                        lenses.Add(lens);
                    }
                    photographer.Add(lenses);
                    photographersXml.Add(photographer);
                }

                xmlDoc.Add(photographersXml);
                xmlDoc.Save("../../../same-cameras-photographers.xml");
            }

        }
    }
}
