namespace Excercises03.Models
{
    using System;

    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int StoreLocationId { get; set; }
        public virtual StoreLocation StoreLocation { get; set; }
        public DateTime? Date { get; set; }
    }
}
