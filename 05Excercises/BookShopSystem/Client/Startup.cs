namespace BookShopSystem
{
    using System;
    using System.Linq;
    using Data;

    class Startup
    {
        static void Main()
        {
            var context = new BookShopContext();

            // Excercise 01 -------------
            /*
            var input = Console.ReadLine().ToLower();
            var books = context.Books
                .Where(b=> b.AgeRestriction.ToString().ToLower() == input )
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine(book.Title);
            }
            */

            // Excercise 02 --------------
            /*
            var books = context.Books
                .Where(b => b.EditionType.ToString().ToLower() == "gold" && b.Copies < 5000)
                .ToList();

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title}");
            }
            */

            // Excercise 03 ----------------
            /*
            var books = context.Books
                .Where(b => b.Price < 5 || b.Price > 40)
                .ToList();

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title} - ${b.Price}");
            }
            */

            // Excercise 04 ---------------------
            /*
            var input = Console.ReadLine();
            var books = context.Books
                .Where(b => b.ReleaseDate.Value.Year.ToString() != input)
                .OrderBy(b=> b.Id)
                .ToList();

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title}");
            }
            */

            // Excercise 05 --------------
            /*
            string[] input = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            foreach (var b in context.Books.OrderBy(b => b.Id))
            {
                if (b.Categories.Any(c=> input.Contains(c.Name.ToLower())))
                {
                    Console.WriteLine(b.Title);
                }
            }
            */

            // Excercise 06 ------------------
            /*
            string input = Console.ReadLine();
            DateTime date = DateTime.ParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            foreach (var book in context.Books)
            {
                if (book.ReleaseDate < date)
                {
                    Console.WriteLine($"{book.Title} - {book.EditionType} - {book.Price}");
                }
            }
            */

            // Excercise 07 ----------------------------
            /*
            string input = Console.ReadLine();

            foreach (var author in context.Authors)
            {
                if (author.FirstName.EndsWith(input))
                {
                    Console.WriteLine($"{author.FirstName} {author.LastName}");
                }
            }
            */

            //Excercise 08 -------------------------
            /*
            string input = Console.ReadLine().ToLower();

            foreach (var book in context.Books)
            {
                if (book.Title.ToLower().Contains(input))
                {
                    Console.WriteLine($"{book.Title}");
                }
            }
            */

            // Excercise 09 --------------------------
            /*
            string input = Console.ReadLine().ToLower();

            foreach (var book in context.Books.OrderBy(b=> b.Id))
            {
                if (book.Author.LastName.ToLower().StartsWith(input))
                {
                    Console.WriteLine($"{book.Title} ({book.Author.FirstName} {book.Author.LastName})");
                }
            }
            */

            //Excercise 10 ---------------------
            /*
            int input = int.Parse(Console.ReadLine());

            int count = context.Books.Count(b => b.Title.Length > input);
            Console.WriteLine($"{count}");
            Console.WriteLine($"There are {count} books with longer title than {input} symbols");
            */

            //Excercise 11 ------------------------
            /*
            var books = context.Books.GroupBy(b => b.Author)
                                    .Select(b => new { Author = b.Key, Copies = b.Sum(c => c.Copies) })
                                    .OrderByDescending(c => c.Copies)
                                    .ToList();

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Author.FirstName} {b.Author.LastName} {b.Copies}");
            }
            */

            //Excercise 12 --------------------
            /*
            var cate = context.Categories
                .GroupBy(c => c.Name)
                .Select(b => new
                {
                    CategoryName = b.Key,
                    Profit = b.Sum(c => c.Books.Sum(d => d.Copies * d.Price))
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.CategoryName)
                .ToList();

            foreach (var cat in cate)
            {
                Console.WriteLine($"{cat.CategoryName} - ${cat.Profit}");
            }
            */

            //Excercise 13 -------------------
            /*
            var categories = context.Categories
                .GroupBy(c => c.Name)
                .Select(c => new {
                    CategoryName = c.Key,
                    BookCount = c.Sum(d => d.Books.Count())
                    })
                .OrderByDescending(c => c.BookCount)
                .ToList();

            foreach (var cat in categories.Where(c=> c.BookCount > 35))
            {
                Console.WriteLine($"--{cat.CategoryName}: {cat.BookCount} books");
                foreach (var book in context.Books
                                        .Where(b => b.Categories.Any(c => cat.CategoryName.Contains(c.Name)))
                                        .OrderByDescending(b => b.ReleaseDate)
                                        .ThenBy(b => b.Title)
                                        .Take(3))
                {
                    Console.WriteLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
                
            }
            */

            // Excercise 14 ------------------------------
            /*
            DateTime date = DateTime.ParseExact("06-06-2013", "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var booksCount = context.Books.Where(b => b.ReleaseDate > date).Count();

            foreach (var book in context.Books.Where(b => b.ReleaseDate > date))
            {
                book.Copies += 44;
            }
            Console.WriteLine("Output = {0}",booksCount * 44);
            Console.WriteLine("Comment: {0} books are released after 6 Jun 2013 so total of {1} book copies were added", booksCount, booksCount * 44);
            context.SaveChanges();
            */

            //Excecise 15 ------------------------------------
            /*
            var booksCount = context.Books.Where(b => b.Copies < 4200).Count();

            foreach (var book in context.Books.Where(b=> b.Copies < 4200))
            {
                context.Books.Remove(book);
            }
            
            Console.WriteLine($"{booksCount} books were deleted");
            context.SaveChanges();
            */
            // Excercise 16 -----------------------------
            
            string[] input = Console.ReadLine().Split(' ').ToArray();
            
            var count = context.Database.SqlQuery<int>("EXEC UDP_BooksByAuthor {0},{1}",input[0], input[1]).First();

            Console.WriteLine($"{input[0]} {input[1]} has written {count} books");
            


        }
    }
}
