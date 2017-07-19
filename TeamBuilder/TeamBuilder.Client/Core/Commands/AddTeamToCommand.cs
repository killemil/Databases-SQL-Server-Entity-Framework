
namespace TeamBuilder.Client.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using TeamBuilder.Client.Utilities;

    public class AddTeamToCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(2, input);
            AuthenticatedManager.Authorize();

            string eventname = input[0];
            string teamName = input[1];

            User currentUser = AuthenticatedManager.GetCurrentUser();

            if (!CommandHelper.IsEventExisting(eventname))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventname));
            }
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }
            if (!CommandHelper.IsUserCreatorOfEvent(eventname,currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }
            if (CommandHelper.IsEventHasTeam(eventname, teamName))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
            }

            this.AddTeamToEvent(eventname, teamName);

            return $"Team {teamName} added for {eventname}!";
        }

        public void AddTeamToEvent(string eventName , string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Event eventt = context.Events
                    .OrderByDescending(e=> e.StartDate)
                    .FirstOrDefault(e=> e.Name == eventName);
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                eventt.Teams.Add(team);
                context.SaveChanges();
            }
        }
    }
}
