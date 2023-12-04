using FitHub.Data;
using FitHub.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CapacityValidation : ValidationAttribute
    {
        private readonly SwimmingPoolContext swimmingPoolContext;
        public CapacityValidation(SwimmingPoolContext swimmingPoolDbContext)
        {
            this.swimmingPoolContext = swimmingPoolDbContext;
        }

        private readonly SaunaContext saunaContext;
        public CapacityValidation(SaunaContext saunaDbContext)
        {
            this.saunaContext = saunaDbContext;
        }

        private readonly SpaContext spaContext;
        public CapacityValidation(SpaContext spaDbContext)
        {
            this.spaContext = spaDbContext;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var booking = (Booking)validationContext.ObjectInstance;
            booking = GetMaxCapacityPerDay(booking);

            DateTime bookingDate = booking.BookingDate;
            string amenityId = booking.AmenityID;
            int numberOfPeople = (int)value;
            int numberReserved = 0;

            if (amenityId == "1")
            {
                numberReserved = GetSwimmingPoolNumberReserved(bookingDate, amenityId);
            }
            else if (amenityId == "2")
            {
                numberReserved = GetSaunaNumberReserved(bookingDate, amenityId);
            }
            else
            {
                numberReserved = GetSpaNumberReserved(bookingDate, amenityId);
            }

            int maxCapacityPerDay = 0;

            if (booking != null && booking.Amenity != null)
            {
                maxCapacityPerDay = booking.Amenity.MaxCapacityPerDay;
            }


            if ((numberOfPeople + numberReserved) > maxCapacityPerDay)
            {
                return new ValidationResult(ErrorMessage);
            }


            if (amenityId == "1")
            {
                UpdateSwimmingPoolSlots(bookingDate, amenityId, numberOfPeople);
            }
            else if (amenityId == "2")
            {
                UpdateSaunaSlots(bookingDate, amenityId, numberOfPeople);
            }
            else
            {
                UpdateSpaSlots(bookingDate, amenityId, numberOfPeople);
            }

            return ValidationResult.Success;
        }

        private int GetSwimmingPoolNumberReserved(DateTime date, string amenityId)
        {
            try
            {
                int numberReserved = 0;
                var swimmingPool = swimmingPoolContext.SwimmingPool
                                .Where(r => r.Date == date)
                                .FirstOrDefault();
                if (swimmingPool != null)
                {
                    numberReserved = swimmingPool.NumberReserved;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NumberReserved for swimming pool: {ex.Message}");
                return 0;
            }

            return 0;
        }

        private int GetSaunaNumberReserved(DateTime date, string amenityId)
        {
            try
            {
                int numberReserved = 0; 
                var sauna = saunaContext.Sauna
                            .Where(r => r.Date == date)
                            .FirstOrDefault();
                if (sauna != null)
                {
                    numberReserved = sauna.NumberReserved;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NumberReserved for sauna: {ex.Message}");
                return 0;
            }

            return 0;
        }

        private int GetSpaNumberReserved(DateTime date, string amenityId)
        {
            try
            {
                int numberReserved = 0;
                var spa = spaContext.Spa
                            .Where(r => r.Date == date)
                            .FirstOrDefault();
                if (spa != null)
                {
                    numberReserved = spa.NumberReserved;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving NumberReserved for spa: {ex.Message}");
                return 0;
            }

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

        private void UpdateSwimmingPoolSlots(DateTime bookingDate, string amenityId, int numberOfPeople)
        {
            try
            {
                var swimmingPool = swimmingPoolContext.SwimmingPool
                                .Where(r => r.Date == bookingDate)
                                .FirstOrDefault();

                if (swimmingPool != null)
                {
                    swimmingPool.NumberReserved = swimmingPool.NumberReserved + numberOfPeople;
                }
                else
                {
                    swimmingPoolContext.SwimmingPool.Add(new SwimmingPool
                    {
                        Date = bookingDate,
                        NumberReserved = numberOfPeople
                    });
                }

                swimmingPoolContext.SaveChanges();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error updating NumberReserved for swimming pool: {ex.Message}");
            }
        }

        private void UpdateSaunaSlots(DateTime bookingDate, string amenityId, int numberOfPeople)
        {
            try
            {
                var sauna = saunaContext.Sauna
                            .Where(r => r.Date == bookingDate)
                            .FirstOrDefault();

                if (sauna != null)
                {
                    sauna.NumberReserved = sauna.NumberReserved + numberOfPeople;
                }
                else
                {
                    saunaContext.Sauna.Add(new Sauna
                    {
                        Date = bookingDate,
                        NumberReserved = numberOfPeople
                    });
                }

                saunaContext.SaveChanges();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error updating NumberReserved for sauna: {ex.Message}");
            }
        }

        private void UpdateSpaSlots(DateTime bookingDate, string amenityId, int numberOfPeople)
        {
            try
            {
                var spa = spaContext.Spa
                            .Where(r => r.Date == bookingDate)
                            .FirstOrDefault();

                if (spa != null)
                {
                    spa.NumberReserved = spa.NumberReserved + numberOfPeople;
                }
                else
                {
                    spaContext.Spa.Add(new Spa
                    {
                        Date = bookingDate,
                        NumberReserved = numberOfPeople
                    });
                }

                spaContext.SaveChanges();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error updating NumberReserved for spa: {ex.Message}");
            }
        }
    }
}
