namespace TeamBuilder.Client.Core.Commands
{
    using Data;
    using System;
    using System.Linq;
    using TeamBuilder.Client.Utilities;
    using TeamBuilder.Models;

    public class DeclineInviteCommand
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

            if (!CommandHelper.IsInviteExisting(teamnName, user))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamnName));
            }


            return $"Invite from {teamnName} declined.";
        }

        public void DeclineInvite(string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                Invitation invitation = context.Invitations
                    .FirstOrDefault(i => i.TeamId == team.Id && i.InvitedUserId == user.Id);
                invitation.IsActive = false;

                context.SaveChanges();
            }
        }
    }
}
