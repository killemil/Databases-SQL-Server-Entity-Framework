namespace Excercises03.Migrations
{
    using System.Data.Entity.Migrations;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SalesDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Excercises03.SalesDbContext";
        }

        protected override void Seed(SalesDbContext context)
        {
            context.Products.AddOrUpdate(new Product() { ProductName = "Shweeps", Quantity = 3, Description = "Soft Drink" });
            context.Products.AddOrUpdate(new Product() { ProductName = "Jameson", Quantity = 2, Description = "Alcohol" });
            context.Products.AddOrUpdate(new Product() { ProductName = "Stolichno", Quantity = 20, Description = "Beer" });

            context.Customers.AddOrUpdate(new Customer() { FirstName = "Ivan", LastName = "Ivanov", Email = "ivan@abv.bg" });
            context.Customers.AddOrUpdate(new Customer() { FirstName = "Stamat", LastName = "Petkanov", Email = "Stamat@gbg.bg" });
            context.Customers.AddOrUpdate(new Customer() { FirstName = "Goran", LastName = "Gospodinov", Email = "Gor4o@ssm.bg" });
        }
    }
}
