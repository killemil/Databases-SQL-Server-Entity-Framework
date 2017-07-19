namespace BusSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        [ForeignKey("Customer")]
        public int OwnerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
