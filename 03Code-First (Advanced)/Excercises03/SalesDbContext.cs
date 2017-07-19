namespace Excercises03
{
    using System.Data.Entity;
    using Models;
    using Migrations;

    public class SalesDbContext : DbContext
    {
        public SalesDbContext()
            : base("name=SalesDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesDbContext,Configuration>());
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<StoreLocation> Locations { get; set; }
    }
}