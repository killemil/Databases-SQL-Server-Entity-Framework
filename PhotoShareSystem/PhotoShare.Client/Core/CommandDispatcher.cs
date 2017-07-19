namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using Commands;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters[0];
            commandParameters = commandParameters.Skip(1).ToArray();
            string result = string.Empty;

            switch (command)
            {
               case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand();
                    result = registerUser.Execute(commandParameters);
                    break;
                case "AddTown":
                    AddTownCommand addTown = new AddTownCommand();
                    result = addTown.Execute(commandParameters);
                    break;
                case "ModifyUser":
                    ModifyUserCommand modifyUser = new ModifyUserCommand();
                    result = modifyUser.Execute(commandParameters);
                    break;
                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    result = exit.Execute();
                    break;
                    
            }
            return result;

        }
    }
}
