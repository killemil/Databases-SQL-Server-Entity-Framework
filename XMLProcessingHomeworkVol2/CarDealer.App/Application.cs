namespace CarDealer.App
{
    using System;
    using Data;
    using Models;
    using System.Linq;
    using System.Xml.Linq;
    using System.Collections.Generic;

    public class Application
    {
        public static void Main()
        {
            //ImportSuppliers();
            //ImportParts();
            //ImportCars();
            //ImportCustomers();
            //ImportSales();

            // Excercise 6 ------------------------
            //Excercise 6.1 -----------------------

            // Excercise601();

            //Excercise 6.2 -----------------------

            // Excercise602();

            //Excercise 6.3 -----------------------

            // Excercise603();

            //Excercise 6.4 -----------------------

            //  Excercise604();

            //Excercise 6.5 -----------------------

            // Excercise605();

            //Excercise 6.6 -----------------------

            Excercise606();
        }

        private static void Excercise606()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var sales = context.Sales
                    .Select(s => new
                    {
                        CarMake = s.Car.Make,
                        CarModel = s.Car.Model,
                        CarTravelledDistance = s.Car.TravelledDistance,
                        CustomerName = s.Customer.Name,
                        Discount = s.Discount,
                        Price = s.Car.Parts.Sum(p => p.Price),
                        PriceWithDiscount = s.Car.Parts.Sum(p => p.Price) * (1 - (s.Discount / 100))

                    });

                XDocument xmlDoc = new XDocument();

                XElement salesXml = new XElement("sales");

                foreach (var s in sales)
                {
                    XElement saleXml = new XElement("sale");

                    XElement carXml = new XElement("car");
                    carXml.SetAttributeValue("make", s.CarMake);
                    carXml.SetAttributeValue("model", s.CarModel);
                    carXml.SetAttributeValue("travelled-distance", s.CarTravelledDistance);

                    XElement customerXml = new XElement("customer-name", s.CustomerName);
                    XElement discountXml = new XElement("discount", s.Discount.ToString());
                    XElement priceXml = new XElement("price", s.Price.ToString());
                    XElement priceDiscount = new XElement("price-with-discount", s.PriceWithDiscount.ToString());

                    carXml.Add(customerXml);
                    carXml.Add(discountXml);
                    carXml.Add(priceXml);
                    carXml.Add(priceDiscount);

                    saleXml.Add(carXml);

                    salesXml.Add(saleXml);
                }
                xmlDoc.Add(salesXml);

                xmlDoc.Save("../../sales.xml");
            }
        }

        private static void Excercise605()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var customers = context.Customers
                    .Where(c => c.Sales.Count > 0)
                    .Select(c => new
                    {
                        Name = c.Name,
                        BoughCars = c.Sales.Count(),
                        TotalSpentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price))
                    });

                XDocument xmlDoc = new XDocument();

                XElement customersXml = new XElement("customers");

                foreach (var c in customers)
                {
                    XElement customerXml = new XElement("customer");
                    customerXml.SetAttributeValue("full-name", c.Name);
                    customerXml.SetAttributeValue("bought-cars", c.BoughCars);
                    customerXml.SetAttributeValue("spent-money", c.TotalSpentMoney.ToString());

                    customersXml.Add(customerXml);
                }
                xmlDoc.Add(customersXml);
                xmlDoc.Save("../../customers.xml");
            }
        }

        private static void Excercise604()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Select(c => new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        Mileage = c.TravelledDistance,
                        Parts = c.Parts.Select(p => new
                        {
                            Name = p.Name,
                            Price = p.Price
                        })
                    });

                XDocument xmlDoc = new XDocument();

                XElement carsXml = new XElement("cars");

                foreach (var c in cars)
                {
                    XElement carXml = new XElement("car");
                    carXml.SetAttributeValue("make", c.Make);
                    carXml.SetAttributeValue("model", c.Model);
                    carXml.SetAttributeValue("travelled-distance", c.Mileage);

                    XElement partsXml = new XElement("parts");

                    foreach (var p in c.Parts)
                    {
                        XElement partXml = new XElement("part");
                        partXml.SetAttributeValue("name", p.Name);
                        partXml.SetAttributeValue("price", p.Price.ToString());

                        partsXml.Add(partXml);
                    }

                    carXml.Add(partsXml);
                    carsXml.Add(carXml);
                }
                xmlDoc.Add(carsXml);
                xmlDoc.Save("../../carWithParts.xml");
            }
        }

        private static void Excercise603()
        {
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

                XDocument xmlDoc = new XDocument();

                XElement suppliersXml = new XElement("suppliers");

                foreach (var s in suppliers)
                {
                    XElement supplierXml = new XElement("supplier");
                    supplierXml.SetAttributeValue("id", s.Id.ToString());
                    supplierXml.SetAttributeValue("name", s.Name);
                    supplierXml.SetAttributeValue("parts-count", s.PartsCount.ToString());

                    suppliersXml.Add(supplierXml);
                }
                xmlDoc.Add(suppliersXml);
                xmlDoc.Save("../../suppliers.xml");
            }
        }

        private static void Excercise602()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c => c.Make == "Ferrari")
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .ToList();

                XDocument xmlDoc = new XDocument();

                XElement carsXml = new XElement("cars");

                foreach (var c in cars)
                {
                    XElement carXml = new XElement("car");
                    carXml.SetAttributeValue("id", c.Id);
                    carXml.SetAttributeValue("model", c.Model);
                    carXml.SetAttributeValue("travelled-distance", c.TravelledDistance.ToString());

                    carsXml.Add(carXml);
                }
                xmlDoc.Add(carsXml);
                xmlDoc.Save("../../carsFerrari.xml");
            }
        }

        private static void Excercise601()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c => c.TravelledDistance > 2000000)
                    .OrderBy(c => c.Make)
                    .ThenBy(c => c.Model)
                    .ToList();

                XDocument xmlDoc = new XDocument();
                XElement carsXml = new XElement("cars");


                foreach (var c in cars)
                {
                    XElement carXml = new XElement("car");

                    XElement makeXml = new XElement("make", c.Make);
                    XElement modelXml = new XElement("model", c.Model);
                    XElement mileage = new XElement("travelled-distance", c.TravelledDistance.ToString());

                    carXml.Add(makeXml);
                    carXml.Add(modelXml);
                    carXml.Add(mileage);

                    carsXml.Add(carXml);
                }

                xmlDoc.Add(carsXml);

                xmlDoc.Save("../../cars.xml");
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
                decimal discount = 0m;
                foreach (var sale in sales)
                {
                    sale.Car = cars.Where(c => c.Id == num % cars.Count()).First();

                    if (discount > 50m)
                    {
                        discount = 0m;
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
                XDocument xmlDoc = XDocument.Load("../../Import/customers.xml");

                var customersXml = xmlDoc.Root.Elements();

                List<Customer> customers = new List<Customer>();

                foreach (var c in customersXml)
                {
                    Customer customer = new Customer()
                    {
                        Name = c.Attribute("name").Value,
                        BirthDate = DateTime.Parse(c.Element("birth-date").Value),
                        IsYoungDriver = bool.Parse(c.Element("is-young-driver").Value)
                    };
                    customers.Add(customer);
                }
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        private static void ImportCars()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                XDocument xmlDoc = XDocument.Load("../../Import/cars.xml");

                var carsXml = xmlDoc.Root.Elements();

                List<Car> cars = new List<Car>();
                List<Part> parts = context.Parts.ToList();

                int num = 0;
                foreach (var c in carsXml)
                {
                    if (num > parts.Count() - 13)
                    {
                        num = 0;
                    }
                    Car car = new Car
                    {
                        Make = c.Element("make").Value,
                        Model = c.Element("model").Value,
                        TravelledDistance = long.Parse(c.Element("travelled-distance").Value),
                        Parts = parts.Skip(num).Take(12).ToList()
                    };

                    cars.Add(car);
                    num++;
                }
                context.Cars.AddRange(cars);
                context.SaveChanges();
            }
        }

        private static void ImportParts()
        {
            using (CarDealerContext context = new CarDealerContext())
            {
                XDocument xmlDoc = XDocument.Load("../../Import/parts.xml");

                var partsXml = xmlDoc.Root.Elements();

                List<Part> parts = new List<Part>();

                int num = 0;
                int supplierCount = context.Suppliers.Count();
                foreach (var p in partsXml)
                {
                    Part part = new Part()
                    {
                        Name = p.Attribute("name").Value,
                        Price = decimal.Parse(p.Attribute("price").Value),
                        Quantity = int.Parse(p.Attribute("quantity").Value),
                        SupplierId = (num % supplierCount) + 1
                    };
                    parts.Add(part);
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
                XDocument xmlDoc = XDocument.Load("../../Import/suppliers.xml");

                var suppliersXml = xmlDoc.Root.Elements();

                List<Supplier> suppliers = new List<Supplier>();


                foreach (var supplier in suppliersXml)
                {
                    Supplier sup = new Supplier()
                    {
                        Name = supplier.Attribute("name").Value,
                        IsImporter = bool.Parse(supplier.Attribute("is-importer").Value)
                    };
                    suppliers.Add(sup);
                }
                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
            }
        }
    }
}
