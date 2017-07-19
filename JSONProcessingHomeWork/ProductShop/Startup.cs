namespace ProductShop
{
    using ProductShop.Data;
    using System.Linq;
    using System;
    using System.IO;
    using Models;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    class Startup
    {
        static void Main()
        {
            //ImportUsers();
            //ImportProducts();
            //ImportCategories();

            // 03 Excercise --------------
            // 03.1 ----------------------
            /*
            using (ProductContext context = new ProductContext())
            {
                var products = context.Products
                    .Include("Seller")
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price,
                        Seller = p.Seller.FirtName + " " + p.Seller.LastName
                    });

                string json = JsonConvert.SerializeObject(products, Formatting.Indented);

                Console.WriteLine(json);
            }
            */

            //3.2 ----------------------
            /*
            using (ProductContext context = new ProductContext())
            {
                var users = context.Users.Include("Buyer").Where(u => u.SoldProducts.Count >= 1)
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirtName)
                    .Select(u => new
                    {
                        FirstName = u.FirtName,
                        LastName = u.LastName,
                        SoldProducts = u.SoldProducts.Select(c => new
                        {
                            Name = c.Name,
                            Product = c.Price,
                            BuyerFistName = c.Buyer.FirtName,
                            BuyerLastName = c.Buyer.LastName
                        })
                    }
                    );

                string json = JsonConvert.SerializeObject(users,Formatting.Indented);

                Console.WriteLine(json);
            }
            */

            //Excercise 3.3 --------------------------

            using (ProductContext context = new ProductContext())
            {
                var categories = context.Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        Name = c.Name,
                        ProductsCount = c.Products.Count(),
                        AveragePrice = (c.Products.Average(p => p.Price)).ToString(),
                        TotalRevenue = c.Products.Sum(p => p.Price).ToString()

                    });

                string json = JsonConvert.SerializeObject(categories, Formatting.Indented);

                Console.WriteLine(json);
            }

            // 3.4 ---------------------
            /*
             using (ProductContext context = new ProductContext())
            {
                var categories = context.Users
                    .Where(u => u.SoldProducts.Count() >= 1)
                    .OrderByDescending(u => u.SoldProducts.Count())
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        firstName = u.FirtName,
                        lastName = u.LastName,
                        age = u.Age,
                        SoldProduct = new
                        {
                            count = u.SoldProducts.Count(),
                            products = u.SoldProducts.Select(p=> new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                        }
                    });

                string json = JsonConvert.SerializeObject(categories, Formatting.Indented);

                Console.WriteLine(json);
            }
            */

        }

        private static void ImportCategories()
        {
            using (ProductContext context = new ProductContext())
            {
                string categoriesJson = File.ReadAllText("../../Import/categories.json");

                List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);

                int number = 0;
                int productCount = context.Products.Count();
                foreach (var c in categories)
                {
                    int categoryProductsCount = number % 3;
                    for (int i = 0; i < categoryProductsCount; i++)
                    {
                        c.Products.Add(context.Products.Find((number % productCount) + 1));
                    }
                    number++;
                }

                context.Categories.AddRange(categories);
                context.SaveChanges();

            }
        }

        private static void ImportProducts()
        {
            using (ProductContext context = new ProductContext())
            {
                string productsJsaon = File.ReadAllText("../../Import/products.json");

                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsJsaon);

                int num = 0;
                int userCount = context.Users.Count();
                foreach (var p in products)
                {
                    p.SellerId = (num % userCount) + 1;
                    if (num % 3 != 0)
                    {
                        p.BuyerId = (num * 2 % userCount) + 1;
                    }
                    num++;
                }

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        private static void ImportUsers()
        {
            using (ProductContext context = new ProductContext())
            {
                string usersJson = File.ReadAllText("../../Import/users.json");

                List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
