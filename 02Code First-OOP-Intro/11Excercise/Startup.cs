namespace _11Excercise
{
    using System;
    using System.Linq;

    class Startup
    {
        static void Main()
        {
            // 11 Excercise 
            /*
            var context = new UserDbContext();

            string emailProvider = Console.ReadLine();

            var result = context.Users.Where(u => u.Email.Contains(emailProvider)).ToList();
            foreach (var u in result)
            {

                Console.WriteLine($"{u.Username} {u.Email}");
            }
            */

            // 12 Excercise

            var context = new UserDbContext();

            string date = Console.ReadLine();
            DateTime enteredDate = DateTime.ParseExact(date, "dd MMM yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var result = context.Users.Where(u => u.LastTimeLoggedIn < enteredDate).ToList();
            var count = result.Count();

            foreach (var u in result)
            {
                u.IsDeleted = true;
            }

            foreach (var u in result)
            {
                context.Users.Remove(u);
            }

            context.SaveChanges();

            if (count > 0)
            {
                Console.WriteLine($"{count} users have been deleted");
            }
            else
            {
                Console.WriteLine("No users have been deleted");
            }
        }
    }
}
