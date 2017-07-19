
namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        public Team()
        {
            this.Events = new HashSet<Event>();
            this.Invitations = new HashSet<Invitation>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }
        
        [StringLength(32)]
        public string Description { get; set; }

        [Required]
        [StringLength(3,MinimumLength =3)]
        public string Acronym { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<User> Members { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }


    }
}
