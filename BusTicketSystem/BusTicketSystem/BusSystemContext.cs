namespace BusTicketSystem
{
    using BusSystem.Models;
    using System.Data.Entity;

    public class BusSystemContext : DbContext
    {
        public BusSystemContext()
            : base("name=BusSystemContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<BusSystemContext>());
        }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BusCompany> BusCompanies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>()
                .HasOptional(c => c.BankAccount)
                .WithOptionalDependent(b => b.Customer);


            modelBuilder.Entity<Trip>()
                .HasRequired(t => t.BusCompany)
                .WithRequiredDependent(b => b.Trip);


            base.OnModelCreating(modelBuilder);
        }
    }



}