using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class Patient
{
    private string email;
    public Patient()
    {
        this.Address = new HashSet<Address>();
        this.Visitations = new HashSet<Visitation>();
        this.Diagnoses = new HashSet<Diagnose>();
        this.Medicaments = new HashSet<Medicament>();
    }
    [Key]
    public int PatientId { get; set; }
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }
    [StringLength(60)]
    public string LastName { get; set; }
    public int? AddressId { get; set; }

    public virtual ICollection<Address> Address { get; set; }
    [StringLength(50)]
    public string Email
    {
        get
        {
            return this.email;
        }
        set
        {
            if (EmailValidation(value))
            {
                this.email = value;
            }
            else
            {
                throw new ArgumentException("Invalid Email!");
            }
        }
    }

    public DateTime DateOfBirth { get; set; }
    public byte[] Picture { get; set; }
    public bool HasMedicalInsurance { get; set; }

    public virtual ICollection<Visitation> Visitations { get; set; }

    public virtual ICollection<Diagnose> Diagnoses { get; set; }

    public virtual ICollection<Medicament> Medicaments { get; set; }

    private bool EmailValidation(string value)
    {
        string pattern = @"(?<=^|[\s+])([A-Za-z0-9]+[-.\w]+@([\w]+[-\w]+[.]){1,2}[a-z]+)$";
        bool isValid = Regex.IsMatch(value, pattern);

        return isValid;
    }
}
