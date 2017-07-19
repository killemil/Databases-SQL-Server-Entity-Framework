
namespace WeddingPlanner.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Cash : Present
    {
        [Required]
        public decimal CashAmount { get; set; }
    }
}
