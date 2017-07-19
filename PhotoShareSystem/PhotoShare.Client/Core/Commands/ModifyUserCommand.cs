namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            string username = data[0];
            string property = data[1];
            string newValue = data[2];

            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users.FirstOrDefault(f => f.Username == username);
                if (property == "Passwod")
                {
                    if (ValidatePassword(property))
                    {
                        throw new ArgumentException($"Value is not valid. (Invalid Password)");
                    }
                    user.Password = newValue;
                    context.Users.Attach(user);
                }
                else if (property == "BornTown")
                {
                    if (context.Towns.Any(t=> t.Name.ToLower() == newValue.ToLower()))
                    {
                        throw new ArgumentException($"Value is not valid. Town {newValue} not found!");
                    }
                    user.BornTown.Name = newValue;
                    context.Users.Attach(user);
                }
                else if (property == "CurrentTown")
                {
                    if(context.Towns.Any(t => t.Name.ToLower() == newValue.ToLower()))
                    {
                        throw new ArgumentException($"Value is not valid. Town {newValue} not found!");
                    }
                    user.CurrentTown.Name = newValue;
                    context.Users.Attach(user);
                }
                else
                {
                    throw new ArgumentException($"Property {property} not supported!");
                }
                context.SaveChanges();
            }
        }
        public bool ValidatePassword (string password)
        {
            bool hasLower = false;
            bool hasDigit = false;

            foreach (var c in password)
            {
                if (char.IsLower(c))
                {
                    hasLower = true;
                }
                if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
            }
            if (hasDigit && hasLower)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
