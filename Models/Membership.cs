using System;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FitHub.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitHub.Models
{
    public class Membership
    {
        [Key]
        public string MembershipID { get; set; }

        [ForeignKey("User")]
        [HiddenInput(DisplayValue = true)]
        public string UserID { get; set; }
        [Display(Name = "Membership Type")]
        [Required]
        public string MembershipType { get; set; }

        [Required]
        //[RegularExpression(@"^\d{4}-\d{2}-\d{2}$", 
          //  ErrorMessage = "Date must be in the format 'YYYY-MM-DD'.")]
        [DateNotInPastAttribute(ErrorMessage = 
            "The date must be greater than or equal to the current date.")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [HiddenInput(DisplayValue = true)]
        [Display(Name = "End Date"), DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
       
        [Display(Name = "Amount Paid")]
        public decimal AmountPaid { get; set; }
        

        [ForeignKey("MD")]
        [HiddenInput(DisplayValue = true)]
        public string MembershipTypeID { get; set; }

        public virtual MembershipDetail MD { get; set; }
        public virtual User User { get; set; }
    }
}
