namespace _04Excercise
{
    using System;

    class Startup
    {
        static void Main()
        {
            var studentName = Console.ReadLine();
            while (studentName != "End")
            {
                var student = new Student()
                {
                    Name = studentName
                };

                studentName = Console.ReadLine();
            }
            Console.WriteLine(Student.studentsCount);
        }
    }
}
