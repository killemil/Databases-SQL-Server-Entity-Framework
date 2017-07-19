namespace BusSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Station
    {
        private ICollection<Trip> startStation;
        private ICollection<Trip> endStation;
        public Station()
        {
            this.Reviews = new HashSet<Review>();
            this.StartStation = new HashSet<Trip>();
            this.EndStation = new HashSet<Trip>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int TownId { get; set; }
        public virtual Town Town { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        [InverseProperty("StartStation")]
        public ICollection<Trip> StartStation
        {
            get { return this.startStation; }
            set { this.startStation = value; }
        }

        [InverseProperty("EndStation")]
        public ICollection<Trip> EndStation
        {
            get { return this.endStation; }
            set { this.endStation = value; }
        }
    }
}
