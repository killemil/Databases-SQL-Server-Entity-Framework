namespace XML.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.BoughtProducts = new HashSet<Product>();
            this.SoldProducts = new HashSet<Product>();
            this.Friends = new HashSet<User>();
        }

        public int Id { get; set; }

        public string FirtName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        public virtual ICollection<Product> SoldProducts { get; set; }

        public virtual ICollection<Product> BoughtProducts { get; set; }

    }
}
