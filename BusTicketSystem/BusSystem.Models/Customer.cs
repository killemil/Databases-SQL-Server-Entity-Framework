
namespace BusSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Customer
    {
        public Customer()
        {
            this.Tickets = new HashSet<Ticket>();
            this.Reviews = new HashSet<Review>();
        }
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender? Gender { get; set; }

        [ForeignKey("Town")]
        public int HomeTownId { get; set; }
        public virtual Town Town { get; set; }

        [ForeignKey("BankAccount")]
        public int AccountId { get; set; }
        public virtual BankAccount BankAccount { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
