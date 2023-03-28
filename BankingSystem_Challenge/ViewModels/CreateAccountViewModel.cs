using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem_Challenge.ViewModels
{
    public class CreateAccountViewModel
    {
        public int? CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordAgain { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string IBAN { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "The code must be exactly 6 digits.")]
        public string Passcode { get; set; }

    }
}
