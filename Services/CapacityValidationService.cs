using FitHub.Data;
using FitHub.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Services
{
    public class CapacityValidationService
    {
        private readonly GymDbContext gymDbContext;

        public CapacityValidationService(GymDbContext gymDbContext)
        {
            this.gymDbContext = gymDbContext;
        }

        public bool IsBookingValid(Booking booking)
        {
            booking = GetMaxCapacityPerDay(booking);

            DateTime bookingDate = booking.BookingDate;
            string amenityId = booking.AmenityID;
            int numberOfPeople = booking.NumberOfPeople;
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
                return false;
            }

            return true;
        }

        private int GetSwimmingPoolNumberReserved(DateTime date, string amenityId)
        {
            int numberReserved = 0;
            using (gymDbContext)
            {
                try
                {
                    var swimmingPool = gymDbContext.SwimmingPool
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
            }
            return numberReserved;
        }

        private int GetSaunaNumberReserved(DateTime date, string amenityId)
        {
            int numberReserved = 0;
            using (gymDbContext)
            {
                try
                {
                    var sauna = gymDbContext.Sauna
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
            }
            return numberReserved;
        }

        private int GetSpaNumberReserved(DateTime date, string amenityId)
        {
            int numberReserved = 0;
            using (gymDbContext)
            {
                try
                {
                    var spa = gymDbContext.Spa
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
            }

            return numberReserved;
        }

        private Booking GetMaxCapacityPerDay(Booking booking)
        {
            using (gymDbContext)
            {
                try
                {
                    booking = gymDbContext.Booking
                        .Include(b => b.Amenity)
                        .Where(b => b.BookingID == booking.BookingID)
                        .FirstOrDefault();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading Amenity: {ex.Message}");
                }
            }

            return booking;
        }
    }
}
