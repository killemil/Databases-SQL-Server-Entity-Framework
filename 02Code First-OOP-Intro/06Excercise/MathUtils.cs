namespace _06Excercise
{
    class MathUtils
    {
        public static double Sum(double firstNum, double secondNum)
        {
            return firstNum + secondNum;
        }
        public static double Subtract(double firstNum, double secondNum)
        {
            return firstNum - secondNum;
        }
        public static double Multiply(double firstNum, double secondNum)
        {
            return firstNum * secondNum;
        }
        public static double Divide(double firstNum, double secondNum)
        {
            return firstNum / secondNum;
        }
        public static double Percentage(double totalNumber, double percentage)
        {
            return Divide(Multiply(totalNumber, percentage), 100);
        }
    }
}
