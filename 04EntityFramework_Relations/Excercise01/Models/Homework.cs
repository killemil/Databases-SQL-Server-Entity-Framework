namespace Excercise01.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public enum ContentType
    {
        Application,
        pdf,
        zip
    }

    public class Homework
    {
        public int HomeworkId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public ContentType ContentType { get; set; }
        [Required]
        public DateTime SubmissionDate { get; set; }
        
        [ForeignKey("Course")]
        public int CourseId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }
}
