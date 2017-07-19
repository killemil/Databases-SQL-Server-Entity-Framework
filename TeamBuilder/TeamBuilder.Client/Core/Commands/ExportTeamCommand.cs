namespace TeamBuilder.Client.Core.Commands
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using TeamBuilder.Client.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    public class ExportTeamCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(1, input);
            string teamName = input[0];

            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            Team team = this.GetTeamByNameWithMember(teamName);

            this.ExportTeam(team);

            return $"Team {teamName} exported!";
        }

        private void ExportTeam(Team team)
        {
            string json = JsonConvert.SerializeObject(new
            {
                Name = team.Name,
                Acronym = team.Acronym,
                Members = team.Members.Select(m => m.Username)
            }, Formatting.Indented);
            
            File.WriteAllText("../../team.json", json);
        }

        private Team GetTeamByNameWithMember(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Include("Members").FirstOrDefault(t => t.Name == teamName);
            }
        }
    }
}
