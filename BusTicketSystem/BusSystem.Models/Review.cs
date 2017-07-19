namespace BusSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }

        [Range(typeof(float), "1", "10")]
        public float Grade { get; set; }
        public int StationId { get; set; }
        public virtual Station Station { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime PublishedOn { get; set; }
        public virtual BusCompany BusCompany { get; set; }
    }
}
