namespace Excercises03.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StoreLocation
    {
        public StoreLocation()
        {
            this.Sales = new HashSet<Sale>();
        }
        [Key]
        public int Id { get; set; }
        public string LocationName { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
