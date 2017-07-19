namespace Excercise11
{
    using Core;
    using System;

    class Startup
    {
        static void Main()
        {
            CommandExecuter excecutor = new CommandExecuter();

            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string output = excecutor.Execute(input);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
