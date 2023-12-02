using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Models
{
    public class Booking
    {
        [Key]
        [Required(ErrorMessage = "Booking Id is Required")]
        [Display(Name = "Booking Id")]
        public string BookingID { get; set; }

        //[ForeignKey("User")]
        [Required(ErrorMessage = "User Id is Required")]
        [Display(Name = "User Id")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Amenity Id is Required")]
        [Display(Name = "Amenity Id")]
        public string AmenityID { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Slot Number is Required")]
        [Display(Name = "Slot Number")]
        public string SlotNumber { get; set; }

        [Required(ErrorMessage = "Number Of People is Required")]
        [Display(Name = "Number Of People")]
        public string NumberOfPeople { get; set; }

        [Required(ErrorMessage = "Amount Paid is Required")]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount Paid")]
        public decimal AmountPaid { get; set; }

    }
}


