namespace Excercise11
{
    using System.Data.Entity;
    using Models;

    public class BankContext : DbContext
    {
        public BankContext()
            : base("name=BankContext")
        {
        }

        public virtual DbSet<SavingAccount> SavingAccounts { get; set; }
        public virtual DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}