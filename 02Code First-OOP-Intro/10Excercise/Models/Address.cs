using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Address
{
    public Address()
    {
        this.Patients = new HashSet<Patient>();
    }
    [Key]
    public int AddressId { get; set; }
    [Required]
    [StringLength(60, MinimumLength = 5)]
    public string AddressText { get; set; }
    public int? TownId { get; set; }

    public virtual Town Town { get; set; }
    public virtual ICollection<Patient> Patients { get; set; }
}

