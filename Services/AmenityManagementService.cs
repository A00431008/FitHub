using FitHub.Data;
using FitHub.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace FitHub.Services
{
    public class AmenityManagementService
    {
        private readonly GymDbContext gymDbContext;

        public AmenityManagementService(GymDbContext gymDbContext)
        {
            this.gymDbContext = gymDbContext;
        }

        public bool IsBookingValid(Booking booking)
        {

            DateTime bookingDate = booking.BookingDate;
            string amenityId = booking.AmenityID;
            int numberOfPeople = booking.NumberOfPeople;
            int numberReserved = 0;

            if (amenityId == "1")
            {
                numberReserved = GetSwimmingPoolNumberReserved(bookingDate, amenityId);
            }
            /*else if (amenityId == "2")
            {
                numberReserved = GetSaunaNumberReserved(bookingDate, amenityId);
            }
            else
            {
                numberReserved = GetSpaNumberReserved(bookingDate, amenityId);
            }*/

            int maxCapacityPerDay = GetMaxCapacityPerDay(amenityId);

            if ((numberOfPeople + numberReserved) > maxCapacityPerDay)
            {
                return false;
            }

            return true;
        }

        public void UpdateAmenityCapacity(Booking booking)
        {
            string amenityId = booking.AmenityID;
            int numberOfPeople = booking.NumberOfPeople;
            DateTime bookingDate = booking.BookingDate;

            if (amenityId == "1")
            {
                UpdateSwimmingPoolSlots(bookingDate, numberOfPeople);
            }
        }

        private int GetSwimmingPoolNumberReserved(DateTime date, string amenityId)
        {
            int numberReserved = 0;
            //using (gymDbContext)
            //{
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
            //}
            return numberReserved;
        }

        /*private int GetSaunaNumberReserved(DateTime date, string amenityId)
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
        }*/

        private int GetMaxCapacityPerDay(string amenityId)
        {
            int maxCapacityPerDay = 0;
            //using (gymDbContext)
            //{
                try
                {
                    var amenity = gymDbContext.Amenity
                        .Where(r => r.AmenityID == amenityId)
                        .FirstOrDefault();

                    if (amenity != null)
                    {
                        maxCapacityPerDay = amenity.MaxCapacityPerDay; 
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading Amenity: {ex.Message}");
                }
            //}

            return maxCapacityPerDay;
        }

        private void UpdateSwimmingPoolSlots(DateTime bookingDate, int numberOfPeople)
        {
            //using (gymDbContext)
            //{
                try
                {
                    var swimmingPool = gymDbContext.SwimmingPool
                                    .Where(r => r.Date == bookingDate)
                                    .FirstOrDefault();

                    if (swimmingPool != null)
                    {
                        swimmingPool.NumberReserved = swimmingPool.NumberReserved + numberOfPeople;
                    }
                    else
                    {
                        gymDbContext.SwimmingPool.Add(new SwimmingPool
                        {
                            Date = bookingDate,
                            NumberReserved = numberOfPeople,
                            AmenityId = 1
                        });
                    }

                    gymDbContext.SaveChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating NumberReserved for swimming pool: {ex.Message}");
                }
            //}
        }

        /*private void UpdateSaunaSlots(DateTime bookingDate, string amenityId, int numberOfPeople)
        {
            using (gymDbContext)
            {
                try
                {
                    var sauna = gymDbContext.Sauna
                                .Where(r => r.Date == bookingDate)
                                .FirstOrDefault();

                    if (sauna != null)
                    {
                        sauna.NumberReserved = sauna.NumberReserved + numberOfPeople;
                    }
                    else
                    {
                        gymDbContext.Sauna.Add(new Sauna
                        {
                            Date = bookingDate,
                            NumberReserved = numberOfPeople
                        });
                    }

                    gymDbContext.SaveChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating NumberReserved for sauna: {ex.Message}");
                }
            }
        }

        private void UpdateSpaSlots(DateTime bookingDate, string amenityId, int numberOfPeople)
        {

            using (gymDbContext)
            {
                try
                {
                    var spa = gymDbContext.Spa
                                .Where(r => r.Date == bookingDate)
                                .FirstOrDefault();

                    if (spa != null)
                    {
                        spa.NumberReserved = spa.NumberReserved + numberOfPeople;
                    }
                    else
                    {
                        gymDbContext.Spa.Add(new Spa
                        {
                            Date = bookingDate,
                            NumberReserved = numberOfPeople
                        });
                    }

                    gymDbContext.SaveChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating NumberReserved for spa: {ex.Message}");
                }
            }*/
        }
    }
