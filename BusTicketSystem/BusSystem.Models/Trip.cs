namespace BusSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Trip
    {
        public Trip()
        {
            this.Tickets = new HashSet<Ticket>();
        }
        public int Id { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public Status Status { get; set; }
        [ForeignKey("StartStation")]
        public int StartStationId { get; set; }
        public virtual Station StartStation { get; set; }
        [ForeignKey("EndStation")]
        public int EndStationId { get; set; }
        public virtual Station EndStation { get; set; }
        public virtual BusCompany BusCompany { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }

    public enum Status
    {
        Departed,
        Arrived,
        Delayed,
        Cancelled
    }
}
