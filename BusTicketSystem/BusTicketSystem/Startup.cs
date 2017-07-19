namespace BusTicketSystem
{
    class Startup
    {
        static void Main()
        {
            BusSystemContext context = new BusSystemContext();

            context.Database.Initialize(true);
        }
    }
}
