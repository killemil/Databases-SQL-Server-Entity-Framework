namespace _07Excercise
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class GringottsContext : DbContext
    {
        public GringottsContext()
            : base("name=GringottsContext")
        {
        }
        public virtual DbSet<WizardDeposit> WizzardDeposits { get; set; }
    }
}