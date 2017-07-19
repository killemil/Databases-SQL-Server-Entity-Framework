
namespace TeamBuilder.Client.Core.Commands
{
    using System;
    using TeamBuilder.Client.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class CreateTeamCommand
    {
        public string Execute(string[] input)
        {
            //<name> <acronym> <description>


            AuthenticatedManager.Authorize();

            string teamName = input[0];
            string teamAcronym = input[1];
            string description = null;
            if (input.Length == 3)
            {
                description = input[2];
            }
            

            if (CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamExists, teamName));
            }
            if (teamAcronym.Length != 3)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InvalidAcronym, teamAcronym));
            }

            User user = AuthenticatedManager.GetCurrentUser();

            if (description == null)
            {
                this.CreateTeam(teamName, teamAcronym, user);
            }
            else
            {
                this.CreateTeam(teamName, teamAcronym, description, user);
            }
            

            return $"Team {teamName} successfully created!";
        }

        public void CreateTeam(string teamName, string acronym, string desctiption, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = new Team()
                {
                    Name = teamName,
                    Acronym = acronym,
                    CreatorId = user.Id,
                    Description = desctiption
                };

                context.Teams.Add(team);
                context.SaveChanges();
            }
        }
        public void CreateTeam(string teamName, string acronym, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = new Team()
                {
                    Name = teamName,
                    Acronym = acronym,
                    CreatorId = user.Id
                };

                context.Teams.Add(team);
                context.SaveChanges();
            }
        }
    }
}
