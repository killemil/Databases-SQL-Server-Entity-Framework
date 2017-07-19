namespace BookShopSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum EditionType
    {
        Normal,
        Promo,
        Gold
    }

    public enum AgeResriction
    {
        Minor,
        Teen,
        Adult
    }
    public class Book
    {
        private ICollection<Category> categories;
        public Book()
        {
            this.categories = new HashSet<Category>();
            this.RelatedBooks = new HashSet<Book>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public EditionType EditionType { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Copies { get; set; }
        public DateTime? ReleaseDate { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<Category> Categories
        {
            get { return this.categories; }
            set { this.categories = value; }
        }

        public AgeResriction AgeRestriction { get; set; }

        public virtual ICollection<Book> RelatedBooks { get; set; }

    }
}
