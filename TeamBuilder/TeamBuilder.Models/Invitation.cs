
namespace TeamBuilder.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Invitation
    {
        public Invitation()
        {
            this.IsActive = true;
        }

        public int Id { get; set; }

        public int InvitedUserId { get; set; }

        public virtual User InvitedUser { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public bool IsActive { get; set; }

    }
}
