
namespace TeamBuilder.Client
{
    using Core;

    public class Startup
    {
        public static void Main()
        {
            Engine engine = new Engine(new CommandDispatcher());
            engine.Run();
        }
    }
}
