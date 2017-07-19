namespace CarDealer.Client
{
    using CarDealer.Data;
    using System.IO;
    using System;
    using Models;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Linq;

    class Startup
    {
        static void Main()
        {
            //ImportSuppliers();
            //ImportPars();
            //ImportCars();
            //ImportCustomers();
            //ImportSales();

            //Excercise 6.1 -------------------------
            /*
            using (CarDealerContext context = new CarDealerContext())
            {
                var customers = context.Customers
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver)
                    .Select(c => new
                    {
                        Id = c.Id,
                        Name = c.Name,
                        BirthDay = c.BirthDate,
                        IsYoungerDriver = c.IsYoungDriver,
                        Sales = c.Sales.Select(s=> new
                        {
                            SaleId = s.Id,
                            CarId = s.CarId,
                            Discount = s.Discount
                        })
                    });

                string json = JsonConvert.SerializeObject(customers, Formatting.Indented);


                Console.WriteLine(json);
            }
            */

            // Excercise 6.2 ------------------------
            /*
            using (CarDealerContext context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c => c.Make.Equals("Toyota"))
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .Select(c => new
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance

                    });

                string json = JsonConvert.SerializeObject(cars, Formatting.Indented);

                Console.WriteLine(json);
            }
            */

            //Excercise 6.3 -----------------------
            /*
            using (CarDealerContext context = new CarDealerContext())
            {
                var suppliers = context.Suppliers
                    .Where(s => s.IsImporter == false)
                    .Select(s => new
                    {
                        Id = s.Id,
                        Name = s.Name,
                        PartsCount = s.Parts.Count()
                    });

                string json = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

                Console.WriteLine(json);
            }
            */

            //Excercise 6.4 ----------------------
            /*
            using (CarDealerContext context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Select(c => new
                    {
                        car = new
                        {
                            Make = c.Make,
                            Model = c.Model,
                            TravelledDIstance = c.TravelledDistance,
                        },
                        parts = c.Parts.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    });

                string json = JsonConvert.SerializeObject(cars, Formatting.Indented);

                Console.WriteLine(json);
                
            }
            */

            //Excerise 6.5 --------------------------
            /*
            using (CarDealerContext context = new CarDealerContext())
            {
                var customers = context.Customers
                    .Where(c => c.Sales.Count >= 1)
                    .Select(c => new
                    {
                        Fullname = c.Name,
                        BoughtCars = c.Sales.Count(),
                        SpentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(g => g.Price))
                    })
                    .OrderByDescending(c => c.SpentMoney)
                    .ThenByDescending(c => c.BoughtCars);

                string json = JsonConvert.SerializeObject(customers, Formatting.Indented);

                Console.WriteLine(json);
            }
            */

            //Excercise 6.6 -------------------------

            using (CarDealerContext context = new CarDealerContext())
            {
                var sales = context.Sales
                    .Select(s => new
                    {
                        car = new
                        {
                            Make = s.Car.Make,
                            Model = s.Car.Model,
                            TravelledDistance = s.Car.TravelledDistance
                        },
                        CustomerName = s.Customer.Name,
                        Discount = s.Discount,
                        Price = s.Car.Parts.Sum(c => c.Price),
                        PriceWithDiscount = (double)s.Car.Parts.Sum(c => c.Price) - ((double)s.Car.Parts.Sum(c => c.Price) * ((double)s.Discount / 100d))
                    });

                string json = JsonConvert.SerializeObject(sales, Formatting.Indented);

                Console.WriteLine(json);
            }
        }

        private static void ImportSales()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                List<Customer> customers = context.Customers.ToList();
                List<Car> cars = context.Cars.ToList();
                List<Sale> sales = new List<Sale>();

                foreach (var customer in customers)
                {
                    Sale sale = new Sale()
                    {
                        Customer = customer
                    };
                    sales.Add(sale);
                }

                var num = 1;
                float discount = 0f;
                foreach (var sale in sales)
                {
                    sale.Car = cars.Where(c => c.Id == num % cars.Count()).First();

                    if (discount > 50f)
                    {
                        discount = 0f;
                    }
                    sale.Discount = discount;

                    discount += 5;
                    num++;
                }
                context.Sales.AddRange(sales);
                context.SaveChanges();
            }
        }

        private static void ImportCustomers()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                string customersJson = File.ReadAllText("../../Import/customers.json");

                List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(customersJson);

                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        private static void ImportCars()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                string carsJson = File.ReadAllText("../../Import/cars.json");

                List<Car> cars = JsonConvert.DeserializeObject<List<Car>>(carsJson);
                List<Part> parts = context.Parts.ToList();

                int num = 0;
                foreach (var c in cars)
                {
                    if (num > parts.Count() - 13)
                    {
                        num = 0;
                    }
                    var randParts = parts.Skip(num).Take(12).ToList();
                    c.Parts = randParts;
                    num += 12;
                }

                context.Cars.AddRange(cars);
                context.SaveChanges();
            }
        }

        private static void ImportPars()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                string partsJason = File.ReadAllText("../../Import/parts.json");

                List<Part> parts = JsonConvert.DeserializeObject<List<Part>>(partsJason);

                int num = 0;
                int supplierCount = context.Suppliers.Count();
                foreach (var p in parts)
                {
                    p.SupplierId = (num % supplierCount) + 1;
                    num++;
                }

                context.Parts.AddRange(parts);
                context.SaveChanges();
            }
        }

        private static void ImportSuppliers()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                string suppliersJson = File.ReadAllText("../../Import/suppliers.json");

                List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(suppliersJson);

                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
            }
        }
    }
}
