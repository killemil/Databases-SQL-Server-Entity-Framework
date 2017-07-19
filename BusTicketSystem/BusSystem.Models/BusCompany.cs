
namespace BusSystem.Models
{
    using System.Collections.Generic;

    public class BusCompany
    {
        public BusCompany()
        {
            this.Reviews = new HashSet<Review>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public float Rating { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

    }
}
