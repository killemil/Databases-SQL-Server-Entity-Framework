
namespace TeamBuilder.Client.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Linq;
    using TeamBuilder.Client.Utilities;

    public class RegisterUserCommand
    {
        public string Execute(string[] input)
        {
            // RegisterUser < username > < password > < repeat - password > < firstName > < lastName > < age > < gender >
            Check.CheckLength(7, input);

            if (AuthenticatedManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }
            
            string username = input[0];

            if (username.Length < Constants.MinUsernameLength || username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            string password = input[1];

            if ((password.Length < Constants.MinPasswordLength || password.Length > Constants.MaxPasswordLength)
                 || (!password.Any(char.IsDigit) || !password.Any(char.IsUpper)))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            string repeatPassword = input[2];
            if (repeatPassword != password)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            string firstName = input[3];
            string lastName = input[4];

            int age;
            bool IsNumber = int.TryParse(input[5], out age);

            if (!IsNumber || age <= 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            Gender gender;
            bool IsGenderValid = Enum.TryParse(input[6], out gender);

            if (!IsGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            if (CommandHelper.IsUserExisting(username))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            this.RegisterUser(username, password, firstName, lastName, age, gender);

            return $"User {username} was registered successfully!";
        }

        private void RegisterUser(string username, string password, string firstName ,string lastName, int age, Gender gender)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = new User()
                {
                    Username = username,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Gender = gender
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
