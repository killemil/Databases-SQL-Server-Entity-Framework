
namespace TeamBuilder.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;
    using TeamBuilder.Client.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class ImportUsersCommand
    {
        public string Execute(string[] input)
        {
            Check.CheckLength(1, input);

            string filePath = input[0];

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format(Constants.ErrorMessages.FileNotFound, filePath));
            }

            List<User> users;
            try
            {
                users = this.GetUsersFromXml(filePath);
            }
            catch (Exception)
            {
                throw new FormatException(Constants.ErrorMessages.InvalidXmlFormat);
            }

            this.AddUsers(users);

            return $"You have successfully imported {users.Count} users!";
        }

        private void AddUsers(List<User> users)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        private List<User> GetUsersFromXml(string filePath)
        {
            List<User> users = new List<User>();

            XDocument xmlDoc = XDocument.Load(filePath);
            var usersXml = xmlDoc.Root.Elements();

            foreach (var u in usersXml)
            {
                User user = new User()
                {
                    Username = u.Element("username").Value,
                    Password = u.Element("password").Value,
                    FirstName = u.Element("first-name").Value,
                    LastName = u.Element("last-name").Value,
                    Age = Convert.ToInt32(u.Element("age").Value),
                };
                Gender gender;
                bool IsGenderValid = Enum.TryParse(u.Element("gender").Value, true, out gender);

                if (!IsGenderValid)
                {
                    throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
                }

                user.Gender = gender;
                users.Add(user);
            }

            return users;
        }
    }
}
