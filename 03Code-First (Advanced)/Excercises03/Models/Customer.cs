namespace Excercises03.Models
{
    using System.Collections.Generic;
    public class Customer
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string CreditCardNumber { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
