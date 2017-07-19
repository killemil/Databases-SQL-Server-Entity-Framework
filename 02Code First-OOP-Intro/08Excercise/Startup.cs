namespace _08Excercise
{
    using System;

    class Startup
    {
        static void Main(string[] args)
        {
            var user = new User()
            {
                Age = 12,
                Email = "emil@abv.bg",
                IsDeleted = false,
                Username = "Elmo",
                Password = "Passw0rd$",
                RegisteredOn = new DateTime(2005, 12, 12)
            };

            Console.WriteLine($"{user.Username} {user.Password} { user.Email}");
        }
    }
}
