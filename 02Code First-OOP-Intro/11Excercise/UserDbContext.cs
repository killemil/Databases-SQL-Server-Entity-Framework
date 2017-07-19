namespace _11Excercise
{
    using System.Data.Entity;

    public class UserDbContext : DbContext
    {
        public UserDbContext()
            : base("name=UserDbContext")
        {
        }
        public virtual DbSet<User> Users { get; set; }

    }
}