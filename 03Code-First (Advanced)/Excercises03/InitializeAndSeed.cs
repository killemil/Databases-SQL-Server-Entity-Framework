namespace Excercises03
{
    using Models;
    using System.Data.Entity;
    public class InitializeAndSeed : CreateDatabaseIfNotExists<SalesDbContext>
    {
        protected override void Seed(SalesDbContext context)
        {

            var product1 = new Product() { ProductName = "Coca Cola", Quantity = 12, Price = 1.80m };
            var product2 = new Product() { ProductName = "Rezovo", Quantity = 13, Price = 0.80m };
            var product3 = new Product() { ProductName = "Whisky", Quantity = 22, Price = 22.30m };

            var customer1 = new Customer() { FirstName = "Ivan", Email = "vankata@mail.bg", CreditCardNumber = "BGN1231287ZZ" };
            var customer2 = new Customer() { FirstName = "Georgi", Email = "gonzo@levski.bg", CreditCardNumber = "BGN843jj3437RBB" };
            var customer3 = new Customer() { FirstName = "Manol", Email = "manol882@yahoo.bg", CreditCardNumber = "BGN8978950LKJ" };

            var loc1 = new StoreLocation() { LocationName = "Sofia" };
            var loc2 = new StoreLocation() { LocationName = "Burgas" };
            var loc3 = new StoreLocation() { LocationName = "Varna" };

            var sale1 = new Sale() { Product = product1, Customer = customer1, StoreLocation = loc1 };
            var sale2 = new Sale() { Product = product2, Customer = customer2, StoreLocation = loc3 };
            var sale3 = new Sale() { Product = product1, Customer = customer3, StoreLocation = loc3 };
            var sale4 = new Sale() { Product = product3, Customer = customer2, StoreLocation = loc2 };
            var sale5 = new Sale() { Product = product2, Customer = customer3, StoreLocation = loc2 };

            context.Sales.AddRange(new Sale[]
            {
                sale1,
                sale2,
                sale3,
                sale4,
                sale5
            });

             base.Seed(context);
        }
    }
}
