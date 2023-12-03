using System.ComponentModel.DataAnnotations;

namespace FitHub.Models
{
    public class Amenity
    {
        [Key]
        [Required(ErrorMessage = "Amenity Id is Required")]
        public int AmenityId { get; set; }

        [Required(ErrorMessage = "Amenity Name is Required")]
        [StringLength(50, ErrorMessage = "Amenity Name must be at most 50 characters long")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed")]
        public string AmenityName { get; set; }

        [Required(ErrorMessage = "Max Capacity Per Day is Required")]
        [Range(1, 100, ErrorMessage = "The value must be between 1 and 100")]
        public int MaxCapacityPerDay { get; set; }

        [Required(ErrorMessage = "Cost Per Person is Required")]
        [DataType(DataType.Currency)]
        public decimal CostPerPerson { get; set; }
    }
}
