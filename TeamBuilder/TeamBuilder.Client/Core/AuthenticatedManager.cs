
namespace TeamBuilder.Client.Core
{
    using Data;
    using System;
    using System.Linq;
    using TeamBuilder.Models;
    using Utilities;

    public static class AuthenticatedManager
    {
        private static User loggedUser;

        public static void Login(string username, string password)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                if (context.Users.Any(u => u.Username == username && u.Password == password && u.IsDeleted == false))
                {
                    loggedUser = context.Users.FirstOrDefault(u => u.Username == username);
                }

                else
                {
                    throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
                }
            }

        }

        public static void Logout()
        {
            loggedUser = null;
        }

        public static bool IsAuthenticated()
        {
            return loggedUser != null;
        }

        public static void Authorize()
        {
            if (loggedUser == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public static User GetCurrentUser()
        {
            return loggedUser;
        }
    }
}
