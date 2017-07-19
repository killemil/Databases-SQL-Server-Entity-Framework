using System;
using System.Linq;
using TeamBuilder.Client.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.Client.Core.Commands
{
    public class InviteToTeamCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(2, input);

            AuthenticatedManager.Authorize();

            string teamName = input[0];
            string username = input[1];
            User user = AuthenticatedManager.GetCurrentUser();

            if (!CommandHelper.IsUserExisting(username) || !CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            if (!CommandHelper.IsMemberOfTeam(teamName, username) && !CommandHelper.IsUserCreatorOfTeam(teamName,user))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            if (CommandHelper.IsInviteExisting(teamName,user))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
            }

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User userr = context.Users.FirstOrDefault(u => u.Username == username);

                if (CommandHelper.IsUserCreatorOfTeam(teamName, userr))
                {
                    this.AddUserToTeam(teamName, user);
                    return $"Team {teamName} added {username}!";
                }
                else
                {
                    this.SendInviteToUser(teamName, userr);
                    return $"Team {teamName} invited {username}!";
                }
            }
        }

        public void AddUserToTeam(string teamName , User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User creator = context.Users.FirstOrDefault(u => u.Username == user.Username);
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                team.Members.Add(creator);
                context.SaveChanges();
            }
        }

        public void SendInviteToUser(string teamnName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User invitedUser = context.Users.FirstOrDefault(u => u.Username == user.Username);
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamnName);

                Invitation invitation = new Invitation()
                {
                    InvitedUserId = invitedUser.Id,
                    TeamId = team.Id
                };

                context.Invitations.Add(invitation);
                context.SaveChanges();
            }
        }
    }
}
