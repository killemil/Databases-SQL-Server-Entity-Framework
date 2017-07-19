
namespace WeddingPlanner.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class Present
    {
        public int InvitationId { get; set; }

        [NotMapped]
        public virtual Person Owner { get {return  this.Invitation.Guest; } }

        public virtual Invitation Invitation { get; set; }
    }
}
