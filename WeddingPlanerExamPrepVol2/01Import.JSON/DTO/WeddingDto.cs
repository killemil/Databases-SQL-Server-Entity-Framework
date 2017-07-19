namespace _01Import.JSON.DTO
{
    using System;
    using System.Collections.Generic;
    using WeddingPlanner.Models;
    public class WeddingDto
    {
        public string Bride { get; set; }

        public string BrideGroom { get; set; }

        public DateTime? Date { get; set; }

        public string Agency { get; set; }

        public List<GuestDto> Guests { get; set; }
    }
}
