using System.ComponentModel.DataAnnotations;

namespace BankingSystem_Challenge.Validation
{
    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var intValue = double.Parse(value.ToString());
            if (intValue > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "The value must be greater than 0.");
        }
    }
}
