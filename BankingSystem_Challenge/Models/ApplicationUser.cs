using Microsoft.AspNetCore.Identity;

namespace BankingSystem_Challenge.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string address { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
