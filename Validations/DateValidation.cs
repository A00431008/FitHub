using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateNotInFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (DateTime.TryParseExact((string?)value, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    // Extract month, day, and year
                    int month = parsedDate.Month;
                    int day = parsedDate.Day;
                    int year = parsedDate.Year;
                }
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }

    public class DateNotInPastAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && (DateTime)value < DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }

    public class DateEqualToCurrent : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && (DateTime)value != DateTime.Now)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }

    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge) { _minimumAge = minimumAge; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dob = (DateTime)value;
                int age = DateTime.Now.Year - dob.Year;
                if (dob > DateTime.Now.AddYears(-age))
                {
                    age--;
                }

                if ( age < _minimumAge) 
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }

}
