
namespace TeamBuilder.Client.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using TeamBuilder.Client.Utilities;

    public class KickMemberCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(2, input);
            AuthenticatedManager.Authorize();

            string teamName = input[0];
            string username = input[1];
            User user = AuthenticatedManager.GetCurrentUser();

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }
            if (!CommandHelper.IsUserExisting(username))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, username));
            }
            if (!CommandHelper.IsMemberOfTeam(teamName,username))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.NotPartOfTeam, username, teamName));
            }
            if (!CommandHelper.IsUserCreatorOfTeam(teamName,user))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            User creatorOfTeam = CommandHelper.GetCreatorOfTeam(teamName);

            if (creatorOfTeam.Username == username)
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.CommandNotAllowed, "DisbandTeam"));
            }

            this.KickTeamMember(teamName, username);

            return $"User {username} was kicked from {teamName}!";
        }

        public void KickTeamMember(string teamName, string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                User user = context.Users.FirstOrDefault(t => t.Username == username);
                team.Members.Remove(user);

                context.SaveChanges();
            }
        }
    }
}
