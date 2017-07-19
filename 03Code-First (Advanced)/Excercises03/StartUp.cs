namespace Excercises03
{
    class StartUp
    {
        static void Main()
        {
            var context = new SalesDbContext();
            context.Database.Initialize(true);
        }
    }
}
