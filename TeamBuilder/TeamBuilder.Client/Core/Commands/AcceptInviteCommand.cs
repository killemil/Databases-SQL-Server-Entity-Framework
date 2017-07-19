namespace TeamBuilder.Client.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.Client.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class AcceptInviteCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(1, input);
            AuthenticatedManager.Authorize();

            string teamnName = input[0];
            User user = AuthenticatedManager.GetCurrentUser();
            
            if (!CommandHelper.IsTeamExisting(teamnName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamnName));
            }

            if (!CommandHelper.IsInviteExisting(teamnName,user))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamnName));
            }

            this.AcceptInvite(teamnName, user);

            return $"User {user.Username} joined team {teamnName}!";
        }

        public void AcceptInvite(string teamName , User invitedUser)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                User user = context.Users.FirstOrDefault(u => u.Username == invitedUser.Username);

                Invitation invitation = context.Invitations
                    .FirstOrDefault(i => i.InvitedUserId == user.Id && i.TeamId == team.Id);
                invitation.IsActive = false;
                team.Members.Add(user);

                context.SaveChanges();
            }
        }
    }
}
