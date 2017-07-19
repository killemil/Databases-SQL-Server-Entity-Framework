using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Visitation
{
    public Visitation()
    {
        this.Patients = new HashSet<Patient>();
    }
    [Key]
    public int VisitationId { get; set; }
    [StringLength(90)]
    public string Comment { get; set; }
    public DateTime Date { get; set; }
    public int PatientId { get; set; }
    public virtual ICollection<Patient> Patients { get; set; }
    public int DoctorId { get; set; }
    public virtual Doctor Doctor { get; set; }
}
