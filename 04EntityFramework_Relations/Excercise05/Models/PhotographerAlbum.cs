
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excercise05.Models
{
    public enum Role
    {
        Owner = 0,
        Viewer = 1
    }

   public class PhotographerAlbum
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Photographer")]
        public int Photographer_Id { get; set; }

        public virtual Photographer Photographer { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Album")]
        public int Album_AlbumId { get; set; }

        public virtual Album Album { get; set; }

        public Role Role { get; set; }
    }
}
