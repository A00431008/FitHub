using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Models
{
    public class MembershipDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string MembershipTypeID { get; set; }

        [Required]
        public string MembershipTypeName { get; set; }

        [Required]
        [Display(Name = "Duration (in months)")] 
        public int DurationMonths { get; set; }

        [Required]
        public decimal Cost { get; set;}

        [Required]
        public string Description { get; set;}
    }
}
