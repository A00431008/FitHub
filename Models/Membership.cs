using System;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitHub.Models
{
    public class Membership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string MembershipID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }

        public virtual User User { get; set; }

        [Required]
        public string MembershipType { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date"), DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        /*public void CalculateEndDate()
        {
            if (StartDate != null && DurationMonths > 0)
            {
                EndDate = StartDate.AddMonths(DurationMonths);
            }
            else
            {
                // Handle error or default behavior when StartDate or Duration is invalid
                // For example:
                // throw new InvalidOperationException("Invalid Start Date or Duration");
                // or set a default EndDate
                // EndDate = DateTime.Now; // Set it to current date/time
            }
        }
         * 
         * You can call this method whenever you want to update the EndDate. 
         * For instance, in your controller, when creating or updating a 
         * Membership object, you can set the StartDate and DurationMonths, 
         * and then call CalculateEndDate() to compute the EndDate.*/

        [Required]
        [Display(Name = "Amount Paid")]
        public string AmountPaid { get; set; }
    }
}
