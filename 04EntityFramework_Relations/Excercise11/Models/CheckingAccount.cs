namespace Excercise11.Models
{
    public class CheckingAccount
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal TaxFee { get; set; }
        public virtual User User { get; set; }
    }
}
