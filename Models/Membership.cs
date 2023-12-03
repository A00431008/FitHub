using System;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitHub.Validations;

namespace FitHub.Models
{
    public class Membership
    {
        private decimal _AmountPaid;
        private DateTime _EndDate;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string MembershipID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", 
            ErrorMessage = "Date must be in the format 'YYYY-MM-DD'.")]
        [DateNotInPastAttribute(ErrorMessage = 
            "The date must be greater than or equal to the current date.")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date"), DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime EndDate
        {
            get => _EndDate;
            set => _EndDate = StartDate.AddMonths(MD.DurationMonths);
        }
        //public DateTime EndDate
        //{
        //    get
        //    {
        //        return EndDate;
        //    }
        //    set
        //    {
        //        if (MD != null)
        //        {
        //            EndDate = StartDate.AddMonths(MD.DurationMonths);
        //        }
        //        //return StartDate; //doubtful
        //    }
        //}
        

        [Required/*(ErrorMessage = "Amount Paid is Required")*/]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount Paid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal AmountPaid
        {
            get => _AmountPaid;
            set => _AmountPaid = MD.Cost;
        }
        //public decimal AmountPaid
        //{
        //    get
        //    {
        //        return AmountPaid;
        //    }
        //    private set
        //    {
        //        if (MD != null)
        //        {
        //            AmountPaid = MD.Cost;
        //        }
        //    }
        //}

        [ForeignKey("MD")]
        public string MembershipTypeID { get; set; }

        public virtual MembershipDetail MD { get; set; }
        public virtual User User { get; set; }
    }
}
