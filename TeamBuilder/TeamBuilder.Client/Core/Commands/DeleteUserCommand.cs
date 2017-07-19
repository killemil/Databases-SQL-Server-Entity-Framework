namespace TeamBuilder.Client.Core.Commands
{
    using Data;
    using System;
    using System.Linq;
    using TeamBuilder.Client.Utilities;
    using TeamBuilder.Models;

    public class DeleteUserCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(0, input);

            if (!AuthenticatedManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            User user = AuthenticatedManager.GetCurrentUser();

            this.DeleteUser(user.Username);
            AuthenticatedManager.Logout();

            return $"User {user.Username} was deleted successfully!";
        }


        private void DeleteUser(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Username == username);
                user.IsDeleted = true;

                context.SaveChanges();
            }
        }
    }
}
