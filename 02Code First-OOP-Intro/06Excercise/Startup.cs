namespace _06Excercise
{
    using System;

    class Startup
    {
        static void Main()
        {
            var input = Console.ReadLine().Split(' ');

            while (input[0] != "End")
            {
                switch (input[0])
                {
                    case "Sum":
                        Console.WriteLine(MathUtils.Sum(double.Parse(input[1]), double.Parse(input[2])).ToString("F2"));
                        break;
                    case "Subtract":
                        Console.WriteLine(MathUtils.Subtract(double.Parse(input[1]), double.Parse(input[2])).ToString("F2"));
                        break;
                    case "Multiply":
                        Console.WriteLine(MathUtils.Multiply(double.Parse(input[1]), double.Parse(input[2])).ToString("F2"));
                        break;
                    case "Divide":
                        Console.WriteLine(MathUtils.Divide(double.Parse(input[1]), double.Parse(input[2])).ToString("F2"));
                        break;
                    case "Percentage":
                        Console.WriteLine(MathUtils.Percentage(double.Parse(input[1]), double.Parse(input[2])).ToString("F2"));
                        break;
                    default:
                        break;
                }

                input = Console.ReadLine().Split(' ');
            }
        }
    }
}
