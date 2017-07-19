namespace SolarSystem.Client
{
    using SolarSystem.Data;

    public class Startup
    {
        public static void Main()
        {
            SolarSystemContext context = new SolarSystemContext();
            context.Database.Initialize(true);
        }
    }
}
