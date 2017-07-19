namespace Excercise01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

   public enum ResourceType
    {
        Video,
        Presentation,
        Document,
        Other
    }

    public class Resource
    {
        public Resource()
        {
            this.Licenses = new HashSet<License>();
        }
        public int ResourceId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ResourceType Type { get; set; }
        [Required]
        public string Url { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<License> Licenses { get; set; }

    }
}
