using System.ComponentModel.DataAnnotations;

namespace FitHub.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateGreaterThanEqualToCurrent : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                if (date >= DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false; 
        }
    }

    public class DateEqualToCurrent : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                if (date == DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
