namespace _07Excercise
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class WizardDeposit
    {
        private int age;
        private int? magicWandSize;

        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        [Required]
        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Age must be positive number!");
                }

                this.age = value;
            }
        }

        [StringLength(100)]
        public string MagicWandCreator { get; set; }

        public int? MagicWandSize
        {
            get
            {
                return this.magicWandSize;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("Invalid number!");
                }
                this.magicWandSize = value;
            }
        }

        [StringLength(20)]
        public string DepositGroup { get; set; }
        public DateTime? DepositStartDate { get; set; }

        public decimal? DepositAmount { get; set; }
        public double? DepositInterest { get; set; }
        public double? DepositCharge { get; set; }
        public DateTime? DepositExpirationDate { get; set; }
        public bool? IsDepositExpired { get; set; }

    }
}
