namespace Excercise11.Core
{
    using System;
    using Models;
    public static class AutheticationManager
    {
        private static User currentUser;

        public static bool IsAuthenticated()
        {
            return currentUser != null;
        }

        public static void Logout()
        {
            if (!IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }
            currentUser = null;
        }
        public static void Login(User user)
        {
            if (IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first !");
            }
            if (user == null)
            {
                throw new InvalidOperationException("Invalid username!");
            }
            currentUser = user;
        }
        public static User GetCurrentUser()
        {
            return currentUser;
        }
    }
}
