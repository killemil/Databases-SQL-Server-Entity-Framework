namespace Excercise05.Models
{
    using System;
    using System.Collections.Generic;

    public class Picture
    {
        public Picture()
        {
            this.Albums = new HashSet<Album>();
        }
        public int PictureId { get; set; }
        public string Title { get; set; }
        public string Capture { get; set; }
        public string FilePath { get; set; }
        public virtual ICollection<Album> Albums { get; set; }
    }
}
