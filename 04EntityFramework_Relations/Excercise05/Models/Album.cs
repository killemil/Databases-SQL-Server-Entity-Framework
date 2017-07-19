﻿namespace Excercise05.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Album
    {
        public Album()
        {
            this.Pictures = new HashSet<Picture>();
            this.Tags = new HashSet<Tag>();
            this.Photographers = new HashSet<PhotographerAlbum>();
        }
        [Key]
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public string BackgroundColor { get; set; }
        public bool IsPublic { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<PhotographerAlbum> Photographers { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

    }
}
