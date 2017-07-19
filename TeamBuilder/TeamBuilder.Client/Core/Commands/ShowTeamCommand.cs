
namespace TeamBuilder.Client.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using TeamBuilder.Client.Utilities;

    public class ShowTeamCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(1, input);

            string teamName = input[0];

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            return PrintTeam(teamName);
        }

        private string PrintTeam(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Team team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                var members = team.Members.Select(m => m.Username);

                return $"TeamName: {team.Name} TeamAcronym: {team.Acronym} \n Members: {string.Join("\n",members)}";
            }
        }
    }
}
