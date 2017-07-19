namespace PhotographyWorkshop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public class Photographer
    {
        public Photographer()
        {
            this.Lenses = new HashSet<Lens>();
            this.Accesoaries = new HashSet<Accessory>();
            this.WorkshopsParticipate = new HashSet<Workshop>();
            this.WorkshopTrainer = new HashSet<Workshop>();

        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 2)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return $"{this.FirstName} {this.LastName}"; }
        }

        [RegularExpression(@"\+\d{1,3}\/\d{8,10}")]
        public string PhoneNumber { get; set; }

        public int? PrimaryCameraId { get; set; }

        public virtual  Camera PrimaryCamera { get; set; }

        public int? SecondaryCameraId { get; set; }

        public virtual Camera SecondaryCamera { get; set; }

        public virtual ICollection<Lens> Lenses { get; set; }

        public virtual ICollection<Accessory> Accesoaries { get; set; }

        public virtual ICollection<Workshop> WorkshopsParticipate { get; set; }

        public virtual ICollection<Workshop> WorkshopTrainer { get; set; }

    }
}
