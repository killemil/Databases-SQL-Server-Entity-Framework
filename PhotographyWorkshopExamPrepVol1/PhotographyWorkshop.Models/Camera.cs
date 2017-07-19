using PhotographyWorkshop.Models.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotographyWorkshop.Models
{
    public class Camera
    {
        public Camera()
        {
            this.PrimaryCamerasPhotographers = new HashSet<Photographer>();
            this.SecondaryCamerssPhotographes = new HashSet<Photographer>();
        }
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public bool? IsFullFrame { get; set; }

        [MinValue]
        public int MinISO { get; set; }

        public int MaxISO { get; set; }

        public virtual ICollection<Photographer> PrimaryCamerasPhotographers { get; set; }

        public virtual ICollection<Photographer> SecondaryCamerssPhotographes { get; set; }
    }
}
