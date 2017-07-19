
namespace _01Import.JSON.DTO
{
    using System;
    using WeddingPlanner.Models.Enums;

    public class PersonDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleInitial { get; set; }

        public Gender Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
