namespace Excercise01.Models
{
    using System;
    using System.Collections.Generic;

    public class License
    {
        public int LicenseId { get; set; }
        public string Name { get; set; }
        public virtual Resource Resource { get; set; }
    }
}
