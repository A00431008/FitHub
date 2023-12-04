using FitHub.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using FitHub.Data;

namespace FitHub.Models
{
    public class Booking
    {
        [Key]
        [Required(ErrorMessage = "Booking Id is Required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Booking Id")]
        public string BookingID { get; set; }

        [ForeignKey("User")]
        [Required(ErrorMessage = "User Id is Required")]
        [Display(Name = "User Id")]
        public string UserID { get; set; }

        [ForeignKey("Amenity")]
        [Required(ErrorMessage = "Amenity Id is Required")]
        [Display(Name = "Amenity Id")]
        public string AmenityID { get; set; }

        [Required(ErrorMessage = "Booking Date is Required")]
        [DataType(DataType.Date)]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Date must be in the format 'YYYY-MM-DD'.")]
        [DateNotInPastAttribute(ErrorMessage = "The date must be greater than or equal to the current date.")]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Slot Number is Required")]
        [Display(Name = "Slot Number")]
        public string SlotNumber { get; set; }

        [Required(ErrorMessage = "Number Of People is Required")]
        [Display(Name = "Number Of People")]
        [CapacityValidation(ErrorMessage = "Enter valid number of people")]
        public int NumberOfPeople { get; set; }

        [Required(ErrorMessage = "Amount Paid is Required")]
        [DataType(DataType.Currency)]
        [Display(Name = "Amount Paid")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal AmountPaid
        {
            get
            {
                return amountPaid;
            }
            private set
            {
                if (Amenity != null)
                {
                    amountPaid = Amenity.CostPerPerson * NumberOfPeople;
                }
                amountPaid = 0;  
            }
        }

        [Required(ErrorMessage = "Purchased Date is Required")]
        [DataType(DataType.Date)]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "Date must be in the format 'YYYY-MM-DD'.")]
        [DateEqualToCurrent(ErrorMessage = "The date must be equal to the current date.")]
        [HiddenInput(DisplayValue = false)]
        public DateTime PurchasedDate
        {
            get
            {
                return purchasedDate;
            }

            private set
            {
                purchasedDate = DateTime.Now;
            }
        }

        public virtual User User { get; set; }
        public virtual Amenity Amenity { get; set; }

        private DateTime purchasedDate;
        private decimal amountPaid;


    }
}


