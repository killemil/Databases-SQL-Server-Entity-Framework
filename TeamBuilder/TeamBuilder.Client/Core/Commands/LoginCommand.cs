
namespace TeamBuilder.Client.Core.Commands
{
    using System;
    using TeamBuilder.Client.Utilities;

    public class LoginCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(2, input);

            if (AuthenticatedManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            string username = input[0];
            string password = input[1];

            AuthenticatedManager.Login(username, password);

            return $"User {username} successfully logged in!";
        }
    }
}
