namespace Excercise01
{
    using System;
    using System.Data.Entity;
    using Models;
    using System.Globalization;

    public class InitializeAndSeed : CreateDatabaseIfNotExists<StudentSystemContext>
    {
        protected override void Seed(StudentSystemContext context)
        {
            var student1 = new Student() { Name = "Ivan", PhoneNumber = "0888888854", RegisteredOn = DateTime.Now };
            var student2 = new Student() { Name = "Georgi", PhoneNumber = "+359 334235", RegisteredOn = DateTime.ParseExact("02-12-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture) };
            var student3 = new Student() { Name = "Stamat", PhoneNumber = "+35933342123", RegisteredOn = DateTime.ParseExact("25-11-2015", "dd-MM-yyyy", CultureInfo.InvariantCulture) };
            var student4 = new Student() { Name = "Strahil", PhoneNumber = "+49 337548934", RegisteredOn = DateTime.ParseExact("03-03-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture) };
            var student5 = new Student() { Name = "Sevdo", PhoneNumber = "+787 88458734", RegisteredOn = DateTime.ParseExact("15-08-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture) };

            var course1 = new Course()
            {
                Name = "Python for Everybody",
                StartDate = DateTime.ParseExact("01-11-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("30-01-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Price = 260M,
                Description = "Python for beginners",
                Students = new Student[]
                {
                    student1,
                    student2,
                    student5
                }
            };
            var course2 = new Course()
            {
                Name = "Date Structures and Algorithms",
                StartDate = DateTime.ParseExact("01-01-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("01-03-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Price = 360M,
                Students = new Student[]
                {
                    student1,
                    student2,
                    student3
                }
            };
            var course3 = new Course()
            {
                Name = "Fundamentals of Computing",
                StartDate = DateTime.ParseExact("03-07-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("15-09-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Price = 280M,
                Students = new Student[]
                {
                    student1,
                    student2,
                    student5,
                    student4
                }
            };
            var course4 = new Course()
            {
                Name = "Databases Basics",
                StartDate = DateTime.ParseExact("18-01-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact("18-02-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Price = 180M,
                Description = "For beginners",
                Students = new Student[]
                {
                    student1,
                    student2,
                    student5,
                    student4,
                    student3
                }
            };

            context.Resources.Add(new Resource
            {
                Name = "Code Academy",
                Type = ResourceType.Video,
                Url = "https://www.codecademy.com/",
                Course = course1
            });
            context.Resources.Add(new Resource
            {
                Name = "Code Avengers",
                Type = ResourceType.Other,
                Url = "https://www.codeavengers.com/",
                Course = course2

            });
            context.Resources.Add(new Resource
            {
                Name = "First steps in programming",
                Type = ResourceType.Other,
                Url = "https://www.nakov.com",
                Course = course3
            });
            context.Resources.Add(new Resource
            {
                Name = "WPF guides",
                Type = ResourceType.Presentation,
                Url = "https://www.misrosoft.com/",
                Course = course4
            });

            context.Homeworks.Add(new Homework
            {
                Content = "Bla bla bla bla",
                ContentType = ContentType.pdf,
                SubmissionDate = DateTime.Now,
                Student = student5,
                Course = course1
            });
            context.Homeworks.Add(new Homework
            {
                Content = "WPF Presentation",
                ContentType = ContentType.zip,
                SubmissionDate = DateTime.ParseExact("01-01-2017", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Student = student4,
                Course = course2
            });
            context.Homeworks.Add(new Homework
            {
                Content = "Algorythms",
                ContentType = ContentType.zip,
                SubmissionDate = DateTime.ParseExact("12-12-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Student = student3,
                Course = course3
            });
            context.Homeworks.Add(new Homework
            {
                Content = "Diagrams",
                ContentType = ContentType.Application,
                SubmissionDate = DateTime.ParseExact("08-12-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Student = student2,
                Course = course4
            });
            context.Homeworks.Add(new Homework
            {
                Content = "SQL Queries",
                ContentType = ContentType.pdf,
                SubmissionDate = DateTime.ParseExact("14-12-2016", "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Student = student1,
                Course = course3

            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
