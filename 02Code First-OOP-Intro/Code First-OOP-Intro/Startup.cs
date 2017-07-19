namespace Code_First_OOP_Intro
{
    class Startup
    {
        public static void Main()
        {
            var firstPerson = new Person()
            {
                Name = "Pesho",
                Age = 20
            };

            var secondPerson = new Person()
            {
                Name = "Gosho",
                Age = 18
            };

            var thirdPerson = new Person()
            {
                Name = "Ivan",
                Age = 29
            };
        }
    }
}
