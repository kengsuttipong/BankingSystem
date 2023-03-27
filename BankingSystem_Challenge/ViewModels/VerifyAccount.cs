using BankingSystem_Challenge.Models;
using BankingSystem_Challenge.Validation;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BankingSystem_Challenge.ViewModels
{
    public class VerifyAccount
    {
        public string? FromName { get; set; }
        public string? FromIban { get; set; }

        public double? Balance { get; set; }

        public string? ToName { get; set; }

        [Required(ErrorMessage = "Destination IBAN can not be blank.")]
        [CheckDestinationIBAN]
        public string ToIBAN { get; set; }

        [Required(ErrorMessage = "Amount can not be blank.")]
        [GreaterThanZero(ErrorMessage = "The number must be greater than 0.")]
        [CheckBalance]
        public double Amount { get; set; }

        public double Fee { get; set; }
        public DateTime DateExcute { get; set; }
    }
}
