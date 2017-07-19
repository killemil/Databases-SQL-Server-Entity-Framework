
namespace WeddingPlanner.Client
{
    using WeddingPlanner.Data;

    public class Startup
    {
        public static void Main()
        {
            WeddingContext context = new WeddingContext();
            context.Database.Initialize(true);
        }
    }
}
