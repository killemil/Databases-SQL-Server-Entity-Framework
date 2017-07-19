namespace PhotographyWorkshop.Client
{
    using PhotographyWorkshop.Data;

    public class Startup
    {
        public static void Main()
        {
            PhotographyContext context = new PhotographyContext();

            context.Database.Initialize(true);
        }
    }
}
