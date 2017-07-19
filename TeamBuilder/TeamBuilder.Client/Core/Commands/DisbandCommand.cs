using System;
using System.Collections.Generic;
using System.Linq;
using TeamBuilder.Client.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.Client.Core.Commands
{
    public class DisbandCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(1, input);
            AuthenticatedManager.Authorize();

            string teamName = input[0];
            User currentUser = AuthenticatedManager.GetCurrentUser();

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }
            if (!CommandHelper.IsUserCreatorOfTeam(teamName,currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            this.RemoveTeam(teamName);

            return $"{teamName} has disbanded!";
        }

        private void RemoveTeam(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                List<Invitation> invitations = context.Invitations.Where(i => i.TeamId == team.Id).ToList();
                List<User> users = context.Users.Where(u => u.Teams.Any(t => t.Name == teamName)).ToList();

                foreach (var u in users)
                {
                    team.Members.Remove(u);
                }

                context.Invitations.RemoveRange(invitations);
                context.Teams.Remove(team);
                context.SaveChanges();
            }
        }
    }
}
