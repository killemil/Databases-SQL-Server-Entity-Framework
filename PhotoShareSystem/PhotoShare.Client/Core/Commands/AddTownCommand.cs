namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                if (context.Towns.Any(t=> t.Name.ToLower() == townName.ToLower()))
                {
                    throw new ArgumentException($"Town {townName} was already been added!");
                }

                context.Towns.Add(town);
                context.SaveChanges();

                return townName + " was added to database!";
            }
        }
    }
}
