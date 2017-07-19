namespace Excercise01
{
    class Startup
    {
        static void Main()
        {
            var waffle = new Product()
            {
                ProductName = "Spoko",
                Description = "Dark Chocolate Waffle",
                Distributor = "Pobeda AD",
                Price = 0.60M
            };

            var biscuit = new Product()
            {
                ProductName = "Everest",
                Description = "Chocolate Biscuits",
                Distributor = "Pobeda Ad",
                Price = 1.20m
            };
            var rakia = new Product()
            {
                ProductName = "Rakia",
                Description = "Grozdova",
                Distributor = "Vinprom Karnobat",
                Price = 6.20m
            };

            var context = new LocalStoreContext();

            context.Products.AddRange(new Product[]
            {
                waffle,
                biscuit,
                rakia
            });
            context.SaveChanges();
        }
    }
}
