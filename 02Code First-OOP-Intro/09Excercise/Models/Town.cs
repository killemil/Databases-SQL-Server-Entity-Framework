using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Town
{
    public Town()
    {
        this.Addresses = new HashSet<Address>();
    }
    [Key]
    public int TownId { get; set; }
    [Required]
    [StringLength(60)]
    public string TownName { get; set; }

    public virtual ICollection<Address> Addresses { get; set; }
}
