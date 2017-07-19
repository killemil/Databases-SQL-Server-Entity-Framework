namespace Excercise01
{
    using System;
    using System.Linq;

    class Startup
    {
        static void Main()
        {
            StudentSystemContext context = new StudentSystemContext();
            context.Database.Initialize(true);

            // 3.1 Excercise -------------

            /* foreach (var student in context.Students)
             {
                 Console.WriteLine($"{student.Name}");
                 foreach (var homework in student.Homeworks)
                 {
                     Console.WriteLine($"--{homework.Content}, {homework.ContentType}");
                 }
             }
             */

            // 3.2 Excercise ---------------

            /*foreach (var course in context.Courses.OrderBy(c=> c.StartDate).ThenByDescending(c=> c.EndDate))
            {
                Console.WriteLine($"{course.Name} {course.Description}");
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine($"--{resource.Name}, {resource.Type}, {resource.Url}");
                }
            }
            */

            // 3.3 Excercise ----------

            /* var result = context.Courses
                .Where(c => c.Resources.Count() > 5)
                .OrderByDescending(c => c.Resources.Count())
                .ThenByDescending(c => c.StartDate);

            foreach (var course in result)
            {
                Console.WriteLine($"{course.Name} {course.Resources.Count()}");
            }
            */

            // 3.4 Excercise ---------
            /*
            string input = Console.ReadLine();
            DateTime date = DateTime.ParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var result = context.Courses.Where(c => c.StartDate <= date && c.EndDate >= date).ToList();

            foreach (var course in result.OrderByDescending(c => c.Students.Count()).ThenByDescending(c => (c.StartDate - c.EndDate).TotalDays))
            {
                var dateDifference = (course.EndDate - course.StartDate).TotalDays;
                Console.WriteLine($@"{course.Name} 
                                     {course.StartDate} 
                                     {course.EndDate} 
                                     {dateDifference} days
                                     {course.Students.Count()} students");
            }
            */

            // 3.5 Excercise ------------

            var result = context.Students
                        .OrderByDescending(s => s.Courses.Sum(c => c.Price))
                        .ThenByDescending(s => s.Courses.Count())
                        .ThenBy(s => s.Name);

            foreach (var stu in result)
            {
                Console.WriteLine($@"{stu.Name} 
                                     {stu.Courses.Count} courses
                                     {stu.Courses.Sum(c => c.Price)} total price
                                     {stu.Courses.Average(c => c.Price)} average price");
            }
        }
    }
}
