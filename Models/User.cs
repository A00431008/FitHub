using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FitHub.Models
{
    public class User
    {
        [Key]
        [Required]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [DisplayName("Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is Required")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public string DOB { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Street Address is Required")]
        [DisplayName("Street Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province or State is Required")]
        [DisplayName("Province or State")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Postal Code is Required")]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
