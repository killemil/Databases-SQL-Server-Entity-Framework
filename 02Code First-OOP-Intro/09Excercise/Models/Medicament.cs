using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Medicament
{
    public Medicament()
    {
        this.Patients = new HashSet<Patient>();
    }
    [Key]
    public int MedicamentId { get; set; }
    [Required]
    [StringLength(60)]
    public string Name { get; set; }
    public virtual ICollection<Patient> Patients { get; set; }
}
