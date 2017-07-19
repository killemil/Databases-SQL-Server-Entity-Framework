namespace _02Excercise
{
    using System;

    class Startup
    {
        static void Main()
        {
            string[] input = Console.ReadLine()
                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (input.Length == 2)
            {
                var name = input[0];
                var age = int.Parse(input[1]);

                var person = new Person(name, age);
                Console.WriteLine($"{person.Name} {person.Age}");
            }
            else if (input.Length == 0)
            {
                var person = new Person();
                Console.WriteLine($"{person.Name} {person.Age}");
            }
            else if (input.Length == 1)
            {
                var name = "";
                var age = -1;
                if (int.TryParse(input[0], out age))
                {
                    var person = new Person(age);
                    Console.WriteLine($"{person.Name} {person.Age}");
                }
                else
                {
                    name = input[0];
                    var person = new Person(name);
                    Console.WriteLine($"{person.Name} {person.Age}");
                }
            }
        }
    }
}
