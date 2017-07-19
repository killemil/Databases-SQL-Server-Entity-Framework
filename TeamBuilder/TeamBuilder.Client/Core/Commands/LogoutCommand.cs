using System;
using TeamBuilder.Client.Utilities;
using TeamBuilder.Models;

namespace TeamBuilder.Client.Core.Commands
{
    public class LogoutCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(0, input);

            if (!AuthenticatedManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            User user = AuthenticatedManager.GetCurrentUser();
            AuthenticatedManager.Logout();

            return $"User {user.Username} successfully logged out!";
        }
    }
}
