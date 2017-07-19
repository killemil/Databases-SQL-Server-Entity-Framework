﻿
namespace WeddingPlanner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Wedding
    {
        public Wedding()
        {
            this.Venues = new HashSet<Venue>();
            this.Invitations = new HashSet<Invitation>();
        }
        public int Id { get; set; }

        [ForeignKey("Bride")]
        public int BrideId { get; set; }

        public virtual Person Bride { get; set; }

        [ForeignKey("Bridegroom")]
        public int BridegroomId { get; set; }

        public virtual Person Bridegroom { get; set; }

        public DateTime? Date { get; set; }

        [ForeignKey("Agency")]
        public int AgencyId { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
    }
}
