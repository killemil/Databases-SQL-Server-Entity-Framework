namespace Excercise11.Models
{
    public class SavingAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public double InterestRate { get; set; }
        public virtual User User { get; set; }
    }
}
