namespace BookShopSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data;
    using System.IO;
    using Models;
    using System.Globalization;

    internal sealed class Configuration : DbMigrationsConfiguration<BookShopSystem.Data.BookShopContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BookShopSystem.Data.BookShopContext";
        }

        protected override void Seed(BookShopSystem.Data.BookShopContext context)
        {
            SeedAuthors(context);
            SeedBooks(context);
            SeedCategories(context);
        }

        private void SeedCategories(BookShopContext context)
        {
            int bookscount = context.Books.Local.Count;
            string[] categories = File.ReadAllLines(@"c:\users\emil\documents\visual studio 2015\Projects\BookShopSystem\BookShopSystem\Import\categories.csv");

            for (int i = 1; i < categories.Length; i++)
            {
                string[] data = categories[i]
                            .Split(',')
                            .Select(c => c.Replace("\"", string.Empty))
                            .ToArray();

                string categoryName = data[0];
                Category category = new Category() { Name = categoryName };

                int bookIndex = (i * 30) % bookscount;
                for (int j = 0; j < bookIndex; j++)
                {
                    Book book = context.Books.Local[j];
                    category.Books.Add(book);
                }

                context.Categories.AddOrUpdate(c => c.Name, category);
            }
        }

        private void SeedBooks(BookShopContext context)
        {
            int authorCount = context.Authors.Local.Count;
            string[] books = File.ReadAllLines(@"c:\users\emil\documents\visual studio 2015\Projects\BookShopSystem\BookShopSystem\Import\books.csv");

            for (int i = 1; i < books.Length; i++)
            {
                string[] data = books[i]
                    .Split(',')
                    .Select(a => a.Replace("\"", string.Empty))
                    .ToArray();

                int authorIndex = i % authorCount;
                Author author = context.Authors.Local[authorIndex];
                EditionType edition = (EditionType)int.Parse(data[0]);
                DateTime realeaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InstalledUICulture);
                int copies = int.Parse(data[2]);
                decimal price = decimal.Parse(data[3]);
                AgeResriction ageRestriction = (AgeResriction)int.Parse(data[4]);
                string title = data[5];

                Book book = new Book()
                {
                    Author = author,
                    AuthorId = author.Id,
                    EditionType = edition,
                    ReleaseDate = realeaseDate,
                    Copies = copies,
                    Price = price,
                    AgeRestriction = ageRestriction,
                    Title = title
                };

                context.Books.AddOrUpdate(b => new { b.Title, b.AuthorId }, book);
            }
        }

        private void SeedAuthors(BookShopContext context)
        {
            string[] authors = File.ReadAllLines(@"c:\users\emil\documents\visual studio 2015\Projects\BookShopSystem\BookShopSystem\Import\authors.csv");

            for (int i = 1; i < authors.Length; i++)
            {
                string[] data = authors[i].Split(',');
                string firstName = data[0].Replace("\"", string.Empty);
                string lastName = data[1].Replace("\"", string.Empty);

                Author author = new Author()
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                context.Authors.AddOrUpdate(a => new { a.FirstName, a.LastName }, author);
            }
        }
    }
}
