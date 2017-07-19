namespace SoftUniDataBase
{
    using Data;
    using System;
    using System.Linq;

    class Startup
    {
        static void Main()
        {
            var context = new SoftUniContext();

            //Excercise 17 ------------------------
            /*
            string[] input = Console.ReadLine().Split(' ');

            var result = context.Database.SqlQuery<ProjectViewModel>("EXEC udp_FindProjectByName {0}, {1}", input[0], input[1]);
            
            foreach (var res in result)
            {
                Console.WriteLine($"{res.Name} - {res.Description}, {res.StartDate}");
            }
            */

            //Excercise 18 -------------------------
            
            var result = context.Departments.
                            GroupBy(d => d.Name)
                            .Select(d => new { DepartmentName = d.Key, MaxSalaray = d.Max(e => e.Employees.Max(f => f.Salary)) })
                            .ToList();

            foreach (var res in result.Where(e=> e.MaxSalaray> 70000 || e.MaxSalaray < 30000))
            {
                Console.WriteLine($"{res.DepartmentName} - {res.MaxSalaray}");
            }
        }
    }
}
