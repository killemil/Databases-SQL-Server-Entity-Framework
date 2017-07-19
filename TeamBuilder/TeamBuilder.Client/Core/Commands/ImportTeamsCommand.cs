
namespace TeamBuilder.Client.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;
    using TeamBuilder.Client.Utilities;

    public class ImportTeamsCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(1, input);

            string filePath = input[0];

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format(Constants.ErrorMessages.FileNotFound, filePath));
            }

            List<Team> teams;

            try
            {
                teams = this.GetTeamsFromXml(filePath);
            }
            catch (Exception)
            {

                throw new FormatException(Constants.ErrorMessages.InvalidXmlFormat);
            }

            this.AddTeams(teams);


            return $"You have successfully imported {teams.Count} teams!";
        }

        private void AddTeams(List<Team> teams)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Teams.AddRange(teams);
                context.SaveChanges();
            }
        }

        private List<Team> GetTeamsFromXml(string filePath)
        {
            List<Team> teams = new List<Team>();

            XDocument xmlDoc = XDocument.Load(filePath);
            var teamsXml = xmlDoc.Root.Elements();

            foreach (var t in teamsXml)
            {
                Team team = new Team()
                {
                    Name = t.Element("name").Value,
                    Acronym = t.Element("acronym").Value,
                    Description = t.Element("description").Value,
                    CreatorId = Convert.ToInt32(t.Element("creator-id").Value)
                };

                teams.Add(team);
            }

            return teams;
        }
    }
}
