﻿namespace _01Import.JSON.DTO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WeddingPlanner.Models.Enums;
    public class GuestDto
    {
        public string Name { get; set; }

        public bool RSVP { get; set; }

        public Family Family { get; set; }
    }
}
