
namespace _05Excercise
{
    class Calculation
    {
        public static double constant = 6.62606896e-34;
        public static double pi = 3.14159;

        public static double ReducedPlankConstant(double constant = 6.62606896e-34, double pi = 3.14159)
        {
            double result = constant / (2 * (pi * constant));

            return result;
        }
    }
}
