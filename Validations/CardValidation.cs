using System.ComponentModel.DataAnnotations;

namespace FitHub.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CardValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? cardNum = value?.ToString();

            if(string.IsNullOrEmpty(cardNum)) {
                return new ValidationResult(ErrorMessage);
            }

            if (cardNum.Length != 15 || cardNum.Length != 16)
            {
                return new ValidationResult(ErrorMessage);
            }

            if ((cardNum.Length == 16) 
                && !(cardNum.StartsWith("51") 
                    || cardNum.StartsWith("52")
                    || cardNum.StartsWith("53")
                    || cardNum.StartsWith("54")
                    || cardNum.StartsWith("55")
                    || cardNum.StartsWith("4"))) 
            {
                return new ValidationResult(ErrorMessage);
                
            } 
            if (cardNum.Length == 15 
                && !(cardNum.StartsWith("34")
                    || cardNum.StartsWith("37")))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
            
        }
    }

    public class ExpiryValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string? expiry = value?.ToString();
            bool hasMonth = int.TryParse(expiry?.Substring(0, 2), out int expiryMonth);
            bool hasYear = int.TryParse(expiry?.Substring(2,4), out int expiryYear);

            if (expiry == null || !hasMonth || !hasYear) 
            { 
                return new ValidationResult("Invalid Expiry Date"); 
            }

            if (expiryMonth < 0 || expiryMonth > 12)
            {
                return new ValidationResult("Month can only be between 01 to 12");
            }

            if (expiryYear < 2016 ||  expiryYear > 2031)
            {
                return new ValidationResult("Year can only be between 2016 and 2031");
            }

            if (expiryMonth < DateTime.Now.Month && expiryYear < DateTime.Now.Year) 
            {
                return new ValidationResult("Card is Expired");
            }

            return ValidationResult.Success;
        }
    }
}
