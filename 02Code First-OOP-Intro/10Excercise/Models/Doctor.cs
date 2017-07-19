using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Doctor
{
    public Doctor()
    {
        this.Visitations = new HashSet<Visitation>();
    }
    [Key]
    public int DoctorId { get; set; }
    [Required]
    [StringLength(60)]
    public string Name { get; set; }
    [Required]
    [StringLength(50)]
    public string Specialty { get; set; }

    public virtual ICollection<Visitation> Visitations { get; set; }
}
