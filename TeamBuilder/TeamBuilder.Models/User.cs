namespace TeamBuilder.Models
{
    using System.ComponentModel.DataAnnotations;
    using Validation;
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.CreatedEvents = new HashSet<Event>();
            this.ReceivedInvitations = new HashSet<Invitation>();
            this.CreatedTeams = new HashSet<Team>();
            this.Teams = new HashSet<Team>();
        }

        public int Id { get; set; }

        [StringLength(25,MinimumLength = 3)]
        [Required]
        public string Username { get; set; }

        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [Password(6, 30, ContainDigit = true , ContainsUppercase = true , ErrorMessage = "Invalid Password")]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Event> CreatedEvents { get; set; }

        public virtual ICollection<Invitation> ReceivedInvitations { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Team> CreatedTeams { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
