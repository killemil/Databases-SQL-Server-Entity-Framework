
namespace WeddingPlanner.Models
{
    using System.ComponentModel.DataAnnotations;
    using WeddingPlanner.Models.Enums;

    public class Gift : Present
    {
        [Required]
        public string Name { get; set; }

        public PresentSize Size { get; set; }
    }
}
