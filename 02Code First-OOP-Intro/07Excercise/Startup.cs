namespace _07Excercise
{
    using System;

    class Startup
    {
        static void Main()
        {
            var context = new GringottsContext();

            // context.Database.Initialize(true);

            WizardDeposit dumbledor = new WizardDeposit()
            {
                FirstName = "Albus",
                LastName = "Dumbledor",
                Age = 150,
                MagicWandCreator = "Antioch Paverell",
                MagicWandSize = 15,
                DepositStartDate = new DateTime(2016, 10, 20),
                DepositExpirationDate = new DateTime(2020, 10, 20),
                DepositAmount = 20000.24M,
                DepositCharge = 0.2,
                IsDepositExpired = false
            };

            WizardDeposit gorendol = new WizardDeposit()
            {
                FirstName = "Elmos",
                LastName = "Gorendol",
                Age = 99,
                MagicWandCreator = "Harachiro",
                MagicWandSize = 12,
                DepositStartDate = new DateTime(2015, 09, 13),
                DepositExpirationDate = new DateTime(2017, 04, 20),
                DepositAmount = 13000.23M,
                DepositCharge = 0.12,
                IsDepositExpired = false
            };
            context.WizzardDeposits.Add(dumbledor);
            context.WizzardDeposits.Add(gorendol);
            context.SaveChanges();
        }
    }
}
