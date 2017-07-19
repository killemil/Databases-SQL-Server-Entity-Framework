using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class User
{
    private string password;
    private int age;
    private string email;
    private bool isDeleted;



    [Key]
    public int Id { get; set; }

    [StringLength(30, ErrorMessage = "Username must be between 4 and 30 symbols", MinimumLength = 4)]
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(60, ErrorMessage = "Password must be between 6 and 50 symbols", MinimumLength = 6)]
    public string Password
    {
        get
        {
            return this.password;
        }
        set
        {
            if (ValidatePassword(value))
            {
                this.password = value;
            }
            else
            {
                throw new ArgumentException("Password must contains at least one lowercase letter, one uppercase letter, one digit and one special symbol ( !,@,#,$,%,^,&,*,(,),-,+,<,>,? )");
            }

        }
    }

    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email
    {
        get
        {
            return this.email;
        }

        set
        {
            if (ValidateEmail(value))
            {
                this.email = value;
            }
            else
            {
                throw new ArgumentException("Invalid Email!");
            }
        }
    }

    public byte[] ProfilePicture { get; set; }

    public DateTime? RegisteredOn { get; set; }

    public DateTime? LastTimeLoggedIn { get; set; }
    
    public int Age
    {
        get
        {
            return this.age;
        }
        set
        {
            if (value > 0 && value <= 120)
            {
                this.age = value;
            }
            else
            {
                throw new ArgumentException("Invalid age!");
            }
        }
    }

    public bool IsDeleted
    {
        get
        {
            return this.isDeleted;
        }
        set
        {
            this.isDeleted = value;
        }
    }
    
    private bool ValidatePassword(string value)
    {
        bool hasUpperCase = false;
        bool hasLowerCase = false;
        bool hasDigit = false;
        bool hasSpecialSymbols = false;

        foreach (char c in value)
        {
            if (char.IsUpper(c))
            {
                hasUpperCase = true;
            }
            else if (char.IsDigit(c))
            {
                hasDigit = true;
            }
            else if (char.IsLower(c))
            {
                hasLowerCase = true;
            }
            else if (c == '!' || c == '@' || c == '#' || c == '$' || c == '%'
                    || c == '^' || c == '&' || c == '*' || c == '(' || c == ')'
                    || c == '_' || c == '+' || c == '<' || c == '>' || c == '?')
            {
                hasSpecialSymbols = true;
            }
        }
        bool isValid = hasUpperCase || hasLowerCase || hasDigit || hasSpecialSymbols;

        return isValid;
    }
    private bool ValidateEmail(string value)
    {
        string pattern = @"(?<=^|[\s+])([A-Za-z0-9]+[-.\w]+@([\w]+[-\w]+[.]){1,2}[a-z]+)$";
        bool isValid = Regex.IsMatch(value, pattern);

        return isValid;
    }
}
