
namespace Excercise11.Models
{
    using System.Collections.Generic;

    public class User
    {
        public User()
        {
            this.CheckingAccounts = new HashSet<CheckingAccount>();
            this.SavingAccounts = new HashSet<SavingAccount>();
        }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CheckingAccount> CheckingAccounts { get; set; }
        public virtual ICollection<SavingAccount> SavingAccounts { get; set; }
    }
}
