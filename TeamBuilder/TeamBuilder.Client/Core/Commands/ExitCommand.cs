
namespace TeamBuilder.Client.Core.Commands
{
    using System;
    using TeamBuilder.Client.Utilities;

    public class ExitCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(0, input);

            Environment.Exit(0);

            return "Bye bye";
        }
    }
}
