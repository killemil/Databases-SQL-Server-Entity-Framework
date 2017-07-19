namespace PhotographyWorkshop.Data.DTO
{
    public class CameraDTO
    {
        public string Type { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public bool IsFullFrame { get; set; }

        public int minISO { get; set; }

        public int MaxISO { get; set; }

        public int MaxShutterSpeed { get; set; }

        public string MaxVideoResolution { get; set; }

        public int MaxFrame { get; set; }

    }
}
