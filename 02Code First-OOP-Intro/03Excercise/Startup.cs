namespace _03Excercise
{
    using System;

    class Startup
    {
        static void Main()
        {
            var numOfRows = int.Parse(Console.ReadLine());
            Family family = new Family();

            for (int i = 0; i < numOfRows; i++)
            {
                string[] input = Console.ReadLine().Split(' ');
                string name = input[0];
                int age = int.Parse(input[1]);

                Person person = new Person(name, age);

                family.AddMember(person);
            }

            Person oldestPerson = family.GetOldestMember();

            Console.WriteLine($"{oldestPerson.Name} {oldestPerson.Age}");
        }
    }
}
