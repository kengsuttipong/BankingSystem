using BankingSystem_Challenge.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BankingSystem_Challenge.Validation
{
    public class CheckBalance : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IServiceProvider serviceProvider = validationContext.GetService(typeof(IServiceProvider)) as IServiceProvider;
            var dbContext = serviceProvider.GetRequiredService<BankingSystemChallengeContext>();
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            string userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var intValue = double.Parse(value.ToString());

            if (intValue > 0)
            {
                var account = dbContext.Accounts.Where(i => i.CustomerId == userId).FirstOrDefault();

                if (account != null)
                {
                    if (intValue > account.Balance)
                    {
                        return new ValidationResult(ErrorMessage ?? "Your balance is less than your amount.");
                    }
                }
                else
                {
                    return new ValidationResult(ErrorMessage ?? "Can not find your bank account.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "The number must be greater than 0.");
        }
    }
}
