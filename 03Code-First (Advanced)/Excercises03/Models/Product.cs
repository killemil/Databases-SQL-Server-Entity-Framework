namespace Excercises03.Models
{
    using System.Collections.Generic;
    public class Product
    {
        public Product()
        {
            this.Sales = new HashSet<Sale>();
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
