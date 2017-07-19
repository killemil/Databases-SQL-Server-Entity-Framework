namespace Excercise11.Core
{
    using System;
    using System.Linq;
    using Models;
    using System.Text;
    using System.Text.RegularExpressions;
    using Utils;

    public class CommandExecuter
    {
        public string Execute (string input)
        {
            string output = "";

            string[] inputArgs = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            string commandName = inputArgs[0].ToLower();
            inputArgs = inputArgs.Skip(1).ToArray();

            switch (commandName)
            {
                case "exit":
                    output = Exit();
                    break;
                case "register":
                    output = RegisterUser(inputArgs);
                    break;
                case "login":
                    output = Login(inputArgs);
                    break;
                case "logout":
                    output = Logout();
                    break;
                case "add":
                    output = AddAccout(inputArgs);
                    break;
                case "deposit":
                    output = DepositMoney(inputArgs);
                    break;
                case "withdraw":
                    output = WithdrawMoney(inputArgs);
                    break;
                case "deductfee":
                    output = DeductFee(inputArgs);
                    break;
                case "addinterest":
                    output = AddInterest(inputArgs);
                    break;
                case "listaccounts":
                    output = ListAccounts();
                    break;
                default:
                    throw new ArgumentException($"Command \"{commandName}\" not supported!");
                    
            }
            return output;
        }

        private string AddAccout(string[] inputArgs)
        {
            if (inputArgs.Length != 3)
            {
                throw new ArgumentException("Input is invalid!");
            }
            if (!AutheticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("You should be logged in first !");
            }

            string accType = inputArgs[0];

            string accNumber = AccountNumberGenerator.GenerateAccountNumber();
            decimal balance = decimal.Parse(inputArgs[1]);
            decimal rateOrFee = decimal.Parse(inputArgs[2]);

            if (accType.Equals("CheckingAccount", StringComparison.OrdinalIgnoreCase))
            {
                CheckingAccount checkingAccount = new CheckingAccount()
                {
                    AccountNumber = accNumber,
                    Balance = balance,
                    TaxFee = rateOrFee
                };

                this.ValidateCheckingAccount(checkingAccount);

                using (BankContext context = new BankContext())
                {
                    User user = AutheticationManager.GetCurrentUser();
                    context.Users.Attach(user);
                    checkingAccount.User = user;

                    context.CheckingAccounts.Add(checkingAccount);
                    context.SaveChanges();
                }
            }

            else if (accType.Equals("SavingAccount",StringComparison.OrdinalIgnoreCase))
            {
                SavingAccount savingAccount = new SavingAccount()
                {
                    AccountNumber = accNumber,
                    Balance = balance,
                    InterestRate = (double)rateOrFee
                };

                this.ValidateSavingAccount(savingAccount);

                using (BankContext context = new BankContext())
                {
                    User user = AutheticationManager.GetCurrentUser();
                    context.Users.Attach(user);
                    savingAccount.User = user;

                    context.SavingAccounts.Add(savingAccount);
                    context.SaveChanges();
                }
            }
            else
            {
                throw new ArgumentException($"Invalid account type {accType}!");
            }
            return $"Account with number {accNumber} has been added";
        }


        private string Exit()
        {
            Environment.Exit(0);

            return string.Empty;
        }

        private string Logout()
        {
            if (!AutheticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first !");
            }

            User user = AutheticationManager.GetCurrentUser();
            AutheticationManager.Logout();

            return $"User {user.Username} successfully logged out!";
        }

        private string Login(string[] input)
        {
            if (input.Length != 2)
            {
                throw new ArgumentException("Input is invalid !");
            }
            if (AutheticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("Yous should logout first !");
            }

            string username = input[0];
            string password = input[1];

            using (BankContext context = new BankContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user == null)
                {
                    throw new ArgumentException("Invalid username or password");
                }
                AutheticationManager.Login(user);
            }
            return $"Succesfully logged in {username}";
        }

        private string RegisterUser(string[] input)
        {
            if (input.Length != 3 )
            {
                throw new ArgumentException("Input is not valid!");
            }
            if (AutheticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("You should logout before login with new user!");
            }

            string username = input[0];
            string password = input[1];
            string email = input[2];

            User user = new User()
            {
                Username = username,
                Password = password,
                Email = email
            };

            this.Validate(user);

            using (BankContext context = new BankContext())
            {
                if (context.Users.Any(u=> u.Username == username))
                {
                    throw new ArgumentException("Username already taken !");
                }
                if (context.Users.Any(u=> u.Email == email))
                {
                    throw new ArgumentException("Email already taken !");
                }
                context.Users.Add(user);
                context.SaveChanges();
            }
            return $"User {username} registered succesfully!";
        }


        private string ListAccounts()
        {
            if(!AutheticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException("You should log in first!");
            }

            StringBuilder builder = new StringBuilder();

            using (BankContext context = new BankContext())
            {
                User user = context.Users.Attach(AutheticationManager.GetCurrentUser());

                builder.AppendLine("Saving Accounts:");
                foreach (SavingAccount account in user.SavingAccounts)
                {
                    builder.AppendLine($"--{account.AccountNumber} {account.Balance}");
                }

                builder.AppendLine("Checking Accounts");
                foreach (CheckingAccount account in user.CheckingAccounts)
                {
                    builder.AppendLine($"--{account.AccountNumber} {account.Balance}");
                }
            }
            return builder.ToString().ToString();
        }

        private string AddInterest(string[] input)
        {
            if (input.Length != 1)
            {
                throw new ArgumentException("Input is nov valid!");
            }

            string accountNumber = input[0];
            decimal currentBalance;

            using (BankContext context = new BankContext())
            {
                SavingAccount savingAccount = context.SavingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (savingAccount == null)
                {
                    throw new ArgumentException($"This {accountNumber} dosnt exist!");
                }
                else
                {
                    savingAccount.Balance += (savingAccount.Balance * (decimal)savingAccount.InterestRate);
                    currentBalance = savingAccount.Balance;
                }
                context.SaveChanges();
            }
            return $"Addet interest to {accountNumber}. Current balance {currentBalance}";
        }

        private string DeductFee(string[] input)
        {
            if (input.Length != 1)
            {
                throw new ArgumentException("Input is not valid !");
            }

            string accountNumber = input[0];
            decimal currentBalance;

            using (BankContext context = new BankContext())
            {
                CheckingAccount chekingAccount = context.CheckingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (chekingAccount == null)
                {
                    throw new ArgumentException($"This {accountNumber} dosnt exist !");
                }
                else
                {
                    chekingAccount.Balance -= chekingAccount.TaxFee;
                    currentBalance = chekingAccount.Balance;
                }

                context.SaveChanges();
            }
            return $"Deducted fee of {accountNumber}. Current balance: {currentBalance}";
        }

        private string WithdrawMoney(string[] input)
        {
            if (input.Length != 2)
            {
                throw new ArgumentException("Input is not valid!");
            }

            string accountNumber = input[0];
            decimal money = decimal.Parse(input[1]);

            decimal currentBalance;

            if (money <= 0)
            {
                throw new ArgumentException("Withdraw amount should be positive number !");
            }

            using (BankContext context = new BankContext())
            {
                SavingAccount savingAccount = context.SavingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                CheckingAccount checkingAccount = context.CheckingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (savingAccount == null && checkingAccount == null)
                {
                    throw new ArgumentException($"This {accountNumber} dosnt exist !");
                }

                if (savingAccount != null)
                {
                    savingAccount.Balance -= money;
                    currentBalance = savingAccount.Balance;
                }
                else
                {
                    checkingAccount.Balance -= money;
                    currentBalance = checkingAccount.Balance;
                }

                context.SaveChanges();
            }
            return $"Account {accountNumber} has balance of {currentBalance}";
        }

        private string DepositMoney(string[] input)
        {
            if (input.Length != 2)
            {
                throw new ArgumentException("Input is not valid!");
            }

            string accountNumber = input[0];
            decimal money = decimal.Parse(input[1]);

            decimal currentBalance;

            if (money <= 0)
            {
                throw new ArgumentException("Deposit should be positive number!");
            }

            using (BankContext context = new BankContext())
            {
                SavingAccount savingAccount = context.SavingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
                CheckingAccount checkingAccount = context.CheckingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (savingAccount == null  && checkingAccount == null)
                {
                    throw new ArgumentException($"This {accountNumber} dosnt exist!");
                }

                if (savingAccount != null)
                {
                    savingAccount.Balance += money;
                    currentBalance = savingAccount.Balance;
                }
                else
                {
                    checkingAccount.Balance += money;
                    currentBalance = checkingAccount.Balance;
                }
                context.SaveChanges();
            }

            return $"Account {accountNumber} has balance of {currentBalance}";
        }

        private void ValidateSavingAccount(SavingAccount account)
        {
            string errors = string.Empty;
            bool isValid = true;

            if (account == null)
            {
                errors = "Account cannot be null";
                throw new ArgumentException(errors);
            }
            if (account.AccountNumber.Length != 10)
            {
                isValid = false;
                errors += "Account number length should be exactly 10 symbols!\n";

            }

            if (account.Balance < 0)
            {
                isValid = false;
                errors += "Account balance should be non-negative!\n";
            }

            if (account.InterestRate < 0)
            {
                isValid = false;
                errors += "Account rate should be non negative! \n";
            }

            if (!isValid)
            {
                throw new ArgumentException(errors.Trim());
            }

        }

        private void ValidateCheckingAccount(CheckingAccount account)
        {
            string error = string.Empty;
            bool isValid = true;

            if (account == null)
            {
                error = "Account cannot be null!";
                throw new ArgumentException(error);
            }

            if (account.AccountNumber.Length != 10)
            {
                isValid = false;
                error += "Account number length should be exactly 10 symbols!\n";

            }

            if (account.Balance < 0)
            {
                isValid = false;
                error += "Account balance should be non-negative!\n";
            }

            if (account.TaxFee < 0)
            {
                isValid = false;
                error += "Account fee should be non negative! \n";
            }

            if (!isValid)
            {
                throw new ArgumentException(error.Trim());
            }

        }

        private void Validate(User user)
        {
            bool isValid = true;
            string error = string.Empty;

            if (user == null)
            {
                error = "user cannot be null";

                throw new ArgumentException(error);
            }

            Regex usernameRegex = new Regex(@"^[a-zA-Z]+[a-zA-Z0-9]{2,}$");
            if (!usernameRegex.IsMatch(user.Username))
            {
                isValid = false;
                error += "Username not valid !\n";
            }

            Regex passwordRegex = new Regex(@"^(?=[a-zA-Z0-9]*[A-Z])(?=[a-zA-Z0-9]*[a-z])(?=[a-zA-Z0-9]*[0-9])[a-zA-Z0-9]{6,}$");
            if (!passwordRegex.IsMatch(user.Password))
            {
                isValid = false;
                error += "Invalid password!\n";
            }

            Regex emailRegex = new Regex(@"^([a-zA-Z0-9]+[-|_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[-]?)*[a-zA-Z0-9]+\.([a-zA-Z0-9]+[-]?)*[a-zA-Z0-9]+$");
            if (!emailRegex.IsMatch(user.Email))
            {
                isValid = false;
                error += "Invalid email!\n";
            }

            if (!isValid)
            {
                throw new ArgumentException(error.Trim());
            }
        }

    }
}
