using BankingSystem_Challenge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BankingSystem_Challenge.Validation
{
    public class CheckDestinationIBAN : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IServiceProvider serviceProvider = validationContext.GetService(typeof(IServiceProvider)) as IServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<BankingSystemChallengeContext>();
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (value is string strValue && !string.IsNullOrEmpty(strValue))
            {
                var accountInfo = dbContext.Accounts.Where(i => i.Iban == strValue).FirstOrDefault();


                if (accountInfo != null)
                {
                    if (accountInfo.CustomerId == userId)
                    {
                        return new ValidationResult(ErrorMessage ?? "Can not transfer to the same IBAN.");
                    }
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? "IBAN is invalid.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "IBAN is blank.");
        }
    }
}
