namespace XML.Client
{
    using Models;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using XML.Data;
    using System;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            //ImportUsers();
            //ImportProducts();
            //ImportCategories();

            //Excercise 4 ------------------------------
            //Excercise 4.1 ----------------------------

            // Excercise401();

            //Excercise 4.2 ----------------------------

            // Excercise402();

            //Excercise 4.3 ----------------------------

            // Excercise403();

            //Excercise 4.4 ----------------------------

            Excercise404();
        }

        private static void Excercise404()
        {
            using (XmlContext context = new XmlContext())
            {
                var users = context.Users
                    .Include("SoldProducts")
                    .Where(u => u.SoldProducts.Count > 0)
                    .OrderByDescending(u => u.SoldProducts.Count())
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        FirstName = u.FirtName,
                        LastName = u.LastName,
                        Age = u.Age,
                        Product = u.SoldProducts.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    });

                XDocument xmlDoc = new XDocument();


                XElement usersXml = new XElement("users");
                usersXml.SetAttributeValue("count", users.Count());

                foreach (var u in users)
                {
                    XElement userXml = new XElement("user");
                    userXml.SetAttributeValue("first-name", u.FirstName);
                    userXml.SetAttributeValue("last-name", u.LastName);
                    userXml.SetAttributeValue("age", u.Age.ToString());

                    XElement soldProductsXml = new XElement("sold-products");
                    soldProductsXml.SetAttributeValue("count", u.Product.Count());

                    foreach (var p in u.Product)
                    {
                        XElement productXml = new XElement("product");
                        productXml.SetAttributeValue("name", p.Name);
                        productXml.SetAttributeValue("price", p.Price);

                        soldProductsXml.Add(productXml);
                    }
                    userXml.Add(soldProductsXml);
                    usersXml.Add(userXml);
                }
                xmlDoc.Add(usersXml);

                xmlDoc.Save("../../SoldProductsByUsers.xml");
            }
        }

        private static void Excercise403()
        {
            using (XmlContext context = new XmlContext())
            {
                var categories = context.Categories
                    .Include("Products")
                    .OrderByDescending(c => c.Products.Count())
                    .Select(c => new
                    {
                        Name = c.Name,
                        ProductsCount = c.Products.Count(),
                        AveragePrice = (c.Products.Sum(p => p.Price) / c.Products.Count()).ToString(),
                        TotalRevenue = c.Products.Sum(p => p.Price).ToString()
                    });

                XDocument xmlDoc = new XDocument();

                XElement categoriesXml = new XElement("categories");

                foreach (var cat in categories)
                {
                    XElement categoryNameXml = new XElement("category");
                    categoryNameXml.SetAttributeValue("name", cat.Name);

                    XElement productCountXml = new XElement("product-coutn", cat.ProductsCount.ToString());
                    XElement averagePriceXml = new XElement("average-price", cat.AveragePrice.ToString());
                    XElement totalRevenueXml = new XElement("total-revenue", cat.TotalRevenue.ToString());

                    categoryNameXml.Add(productCountXml);
                    categoryNameXml.Add(averagePriceXml);
                    categoryNameXml.Add(totalRevenueXml);

                    categoriesXml.Add(categoryNameXml);

                }

                xmlDoc.Add(categoriesXml);
                xmlDoc.Save("../../categories.xml");
            }
        }

        private static void Excercise402()
        {
            using (XmlContext context = new XmlContext())
            {
                var users = context.Users
                    .Where(u => u.SoldProducts.Count() >= 1)
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirtName)
                    .Select(u => new
                    {
                        FirstName = u.FirtName,
                        LastName = u.LastName,
                        SoldProducts = u.SoldProducts
                    });

                XDocument xmlDoc = new XDocument();

                XElement usersXml = new XElement("users");

                foreach (var u in users)
                {
                    XElement userXml = new XElement("user");
                    userXml.SetAttributeValue("first-name", u.FirstName);
                    userXml.SetAttributeValue("last-name", u.LastName);

                    XElement soldProductsXml = new XElement("sold-products");



                    XElement nameXml = new XElement("name");
                    XElement priceXml = new XElement("price");
                    foreach (var product in u.SoldProducts)
                    {
                        XElement productXml = new XElement("product");
                        nameXml.Value = product.Name;
                        priceXml.Value = product.Price.ToString();

                        productXml.Add(nameXml);
                        productXml.Add(priceXml);
                        soldProductsXml.Add(productXml);
                    }

                    userXml.Add(soldProductsXml);

                    usersXml.Add(userXml);
                }

                xmlDoc.Add(usersXml);

                xmlDoc.Save("../../users.xml");
            }
        }

        private static void Excercise401()
        {
            using (XmlContext context = new XmlContext())
            {
                var products = context.Products
                    .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.BuyerId != null)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        ProductName = p.Name,
                        Price = p.Price,
                        Buyer = p.Buyer.FirtName + " " + p.Buyer.LastName
                    });



                XDocument xmlDoc = new XDocument();
                XElement productsXml = new XElement("products");

                foreach (var p in products)
                {
                    XElement productXml = new XElement("product");
                    productXml.SetAttributeValue("name", p.ProductName);
                    productXml.SetAttributeValue("price", p.Price.ToString());
                    productXml.SetAttributeValue("buyer", p.Buyer);


                    productsXml.Add(productXml);


                }
                xmlDoc.Add(productsXml);

                xmlDoc.Save("../../products.xml");

            }
        }

        private static void ImportCategories()
        {
            XDocument xmlDoc = XDocument.Load("../../Import/categories.xml");
            var categoriesXml = xmlDoc.Root.Elements();

            List<Category> categories = new List<Category>();

            foreach (var c in categoriesXml)
            {
                string categoryName = c.Element("name").Value;

                Category category = new Category()
                {
                    Name = categoryName
                };
                categories.Add(category);
            }
            using (XmlContext context = new XmlContext())
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();

                var products = context.Products.ToList();
                int num = 1;
                int categoryCount = context.Categories.Count();
                foreach (var p in products)
                {
                    if (num > categoryCount)
                    {
                        num = 1;
                    }
                    p.Categories = context.Categories.Where(c => c.Id >= num && c.Id < categoryCount).ToList();

                    num++;
                }
                context.SaveChanges();
            }
        }

        private static void ImportProducts()
        {
            using (XmlContext context = new XmlContext())
            {
                XDocument xmlDoc = XDocument.Load("../../Import/products.xml");
                var productsXml = xmlDoc.Root.Elements();

                List<Product> products = new List<Product>();

                foreach (var p in productsXml)
                {
                    string productName = p.Element("name").Value;
                    decimal price = decimal.Parse(p.Element("price").Value);

                    Product product = new Product()
                    {
                        Name = productName,
                        Price = price
                    };
                    products.Add(product);
                }

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
            XDocument xmlDoc = XDocument.Load("../../Import/users.xml");

            var usersXml = xmlDoc.Root.Elements();

            List<User> users = new List<User>();

            foreach (var u in usersXml)
            {

                User user = new User()
                {
                    FirtName = u.Attribute("first-name")?.Value,
                    LastName = u.Attribute("last-name")?.Value,
                    Age = Convert.ToInt32(u.Attribute("age")?.Value)
                };
                users.Add(user);
            }
            using (XmlContext context = new XmlContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
