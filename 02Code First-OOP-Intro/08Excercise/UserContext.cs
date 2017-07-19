namespace _08Excercise
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserContext : DbContext
    {
        // Your context has been configured to use a 'UserContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // '_08Excercise.UserContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'UserContext' 
        // connection string in the application configuration file.
        public UserContext()
            : base("name=UserContext")
        {
        }
        public virtual DbSet<User> Users { get; set; }
    }
}