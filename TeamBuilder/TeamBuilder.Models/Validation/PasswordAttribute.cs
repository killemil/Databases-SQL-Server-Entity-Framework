
namespace TeamBuilder.Models.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    internal class PasswordAttribute : ValidationAttribute
    {
        private int minLength;
        private int maxLength;

        public PasswordAttribute(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public bool ContainsUppercase { get; set; }

        public bool ContainDigit { get; set; }

        public override bool IsValid(object value)
        {
            string password = value.ToString();

            if (password.Length < this.minLength || password.Length > this.maxLength)
            {
                return false;
            }

            if (!password.Any(c=> char.IsUpper(c)) && this.ContainsUppercase)
            {
                return false;
            }

            if (!password.Any(c=> char.IsDigit(c)) && this.ContainDigit) 
            {
                return false;
            }

            return true;
        }
    }
}
