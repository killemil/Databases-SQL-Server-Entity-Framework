namespace Excercise01.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Course>();
            this.Homeworks = new HashSet<Homework>();
        }
        public int StudentId { get; set; }

        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime RegisteredOn { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Homework> Homeworks { get; set; }

    }
}
