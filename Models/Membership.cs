using System;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitHub.Models
{
    public class Membership
    {
        [Key]
        public string MembershipID { get; set; }

        /*[ForeignKey("User")]
        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }*/

        [Required]
        public string MembershipType { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Amount Paid")]
        public string AmountPaid { get; set; }
    }
}
