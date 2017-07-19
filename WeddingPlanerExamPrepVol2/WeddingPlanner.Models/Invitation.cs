
namespace WeddingPlanner.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using WeddingPlanner.Models.Enums;

    public class Invitation
    {
        public int Id { get; set; }

        [ForeignKey("Wedding")]
        public int WeddingId { get; set; }

        [Required]
        public virtual Wedding Wedding { get; set; }

        [ForeignKey("Guest")]
        public int GuestId { get; set; }

        [Required]
        public virtual Person Guest { get; set; }

        public Present Present { get; set; }

        public bool IsAtteding { get; set; }

        public Family Family { get; set; }
    }
}
