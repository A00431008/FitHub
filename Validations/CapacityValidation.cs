using FitHub.Models;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CapacityValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var booking = (Booking)validationContext.ObjectInstance;
            booking = GetMaxCapacityPerDay(booking);

            DateTime bookingDate = booking.BookingDate;
            string amenityId = booking.AmenityID;
            int numberOfPeople = (int)value;

            int numberReserved = GetNumberReserved(bookingDate, amenityId);

            int maxCapacityPerDay = 0;

            if (booking != null && booking.Amenity != null)
            {
                maxCapacityPerDay = booking.Amenity.MaxCapacityPerDay;
            }
            

            if ((numberOfPeople + numberReserved) > maxCapacityPerDay)
            {
                return new ValidationResult(ErrorMessage);
            }

            UpdateNumberReserved(bookingDate, amenityId, numberOfPeople);

            return ValidationResult.Success;
        }

        private int GetNumberReserved(DateTime date, string amenityId)
        {
            /*using (var dbContext = new YourDbContext())
            {
                try
                {
                    int numberReserved = 0;

                    switch (amenityId)
                    {
                        case "1": 
                            var swimmingPool = dbContext.SwimmingPools
                                .Where(r => r.Date == date)
                                .FirstOrDefault();
                            if (swimmingPool != null)
                            {
                                numberReserved = swimmingPool.NumberReserved;
                            }
                            break;

                        case "2": 
                            var sauna = dbContext.Saunas
                                .Where(r => r.Date == date)
                                .FirstOrDefault();
                            if (sauna != null)
                            {
                                numberReserved = sauna.NumberReserved;
                            }
                            break;

                        case "3": 
                            var spa = dbContext.Spas
                                .Where(r => r.Date == date)
                                .FirstOrDefault();
                            if (spa != null)
                            {
                                numberReserved = spa.NumberReserved;
                            }
                            break;

                        default:
                            break;
                    }

                    return numberReserved;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error retrieving NumberReserved: {ex.Message}");
                    return 0;
                }
            }*/
            return 0;
        }

        private Booking GetMaxCapacityPerDay(Booking booking)
        {
            /*using (var dbContext = new YourDbContext())
            {
                try
                {
                    booking = dbContext.Bookings
                        .Include(b => b.Amenity) 
                        .Where(b => b.BookingID == booking.BookingID)
                        .FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading Amenity: {ex.Message}");
                }
            }*/

            return booking;
        }

        private void UpdateNumberReserved(DateTime bookingDate, string amenityId, int numberOfPeople)
        {
            /*using (var dbContext = new YourDbContext())
            {
                try
                {
                    switch (amenityId)
                    {
                        case "1":
                            var swimmingPool = dbContext.SwimmingPools
                                .Where(r => r.Date == bookingDate)
                                .FirstOrDefault();

                            if (swimmingPool != null)
                            { 
                                swimmingPool.NumberReserved = swimmingPool.NumberReserved + numberOfPeople;
                            }
                            else
                            {
                                dbContext.SwimmingPools.Add(new SwimmingPool
                                {
                                    Date = bookingDate,
                                    NumberReserved = numberOfPeople
                                });
                            }
                            break;

                        case "2": 
                            var sauna = dbContext.Saunas
                                .Where(r => r.Date == bookingDate)
                                .FirstOrDefault();

                            if (sauna != null)
                            {
                                sauna.NumberReserved = sauna.NumberReserved + numberOfPeople;
                            }
                            else
                            {
                                dbContext.Saunas.Add(new Sauna
                                {
                                    Date = bookingDate,
                                    NumberReserved = numberOfPeople
                                });
                            }
                            break;

                        case "3": 
                            var spa = dbContext.Spas
                                .Where(r => r.Date == bookingDate)
                                .FirstOrDefault();

                            if (spa != null)
                            {
                                spa.NumberReserved = spa.NumberReserved + numberOfPeople;
                            }
                            else
                            {
                                dbContext.Spas.Add(new Spa
                                {
                                    Date = bookingDate,
                                    NumberReserved = numberOfPeople
                                });
                            }
                            break;

                        default:
                            break;
                    }

                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                     Console.WriteLine($"Error updating NumberReserved: {ex.Message}");
                }
            }*/
        }
    }
}
