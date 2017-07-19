using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Diagnose
{
    public Diagnose()
    {
        this.Patients = new HashSet<Patient>();
    }
    [Key]
    public int DiagnoseId { get; set; }
    [Required]
    [StringLength(30, MinimumLength = 3)]
    public string Name { get; set; }
    [StringLength(200)]
    public string Comment { get; set; }
    [Required]
    public int PatientId { get; set; }
    public virtual ICollection<Patient> Patients { get; set; }
}
