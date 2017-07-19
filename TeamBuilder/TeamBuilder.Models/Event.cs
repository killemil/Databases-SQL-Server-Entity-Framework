
namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        public Event()
        {
            this.Teams = new HashSet<Team>();
        }
        public int Id { get; set; }
        
        [StringLength(25), Required]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<Team> Teams { get; set; }


    }
}
