namespace Excercise02
{
    class Startup
    {
        static void Main()
        {
            var context = new LocalStoreContext();
            context.Database.Initialize(true);
        }
    }
}
