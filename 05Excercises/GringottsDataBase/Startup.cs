namespace GringottsDataBase
{
    using GringottsDataBase.Data;
    using System;
    using System.Linq;

    class Startup
    {
        static void Main()
        {
            var context = new GringottsContext();
            //Excercise 19
            /*
            var result = context.WizzardDeposits
                        .Where(w => w.MagicWandCreator == "Ollivander family")
                        .GroupBy(w => w.DepositGroup)
                        .Select(w => new { DepositGroup = w.Key, TotalSum = w.Sum(d => d.DepositAmount) })
                        .ToList();

            foreach (var res in result)
            {
                Console.WriteLine($"{res.DepositGroup} - {res.TotalSum}");
            }
            */

            //Excercise 20 -------------------------------
            

            var result = context.WizzardDeposits
                            .Where(w => w.MagicWandCreator == "Ollivander family")
                            .GroupBy(w => w.DepositGroup)
                            .Select(w => new { DepositGroup = w.Key, TotalDeposit = w.Sum(s => s.DepositAmount) })
                            .Where(w => w.TotalDeposit < 150000)
                            .OrderByDescending(w=> w.TotalDeposit)
                            .ToList();

            foreach (var res in result)
            {
                Console.WriteLine($"{res.DepositGroup} - {res.TotalDeposit}");
            }
        }
    }
}
