namespace BusSystem.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public Town()
        {
            this.Stations = new HashSet<Station>();
            this.Natives = new HashSet<Customer>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public virtual ICollection<Customer> Natives { get; set; }
        public virtual ICollection<Station> Stations { get; set; }

    }
}
