using FitHub.Data;
using FitHub.Models;
using FitHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FitHub.Controllers
{
    public class PaymentController : Controller
    {

        private readonly GymDbContext _context;
        private readonly AmenityManagementService _amenityManagementService;

        public PaymentController(GymDbContext context, AmenityManagementService amenityManagementService)
        {
            _context = context;
            _amenityManagementService = amenityManagementService;
        }

        //[HttpGet]
        public IActionResult BookingPaymentForm()
        {
            var booking = JsonConvert.DeserializeObject<Booking>(TempData["BookingData"] as String);

            if (booking == null)
            {
                return NotFound();
            }

            return View("BookingPaymentForm", new PaymentMethod { Booking = booking });
            /*// Declare and return an empty payment method form at the beginning
            var paymentMethod = new PaymentMethod();
            return View(paymentMethod);*/
        }

        [HttpPost]
        /*[Route("/api/payment/process")]*/
        public IActionResult ProcessBookingPayment(PaymentMethod paymentMethod)
        {

            ModelState.Remove("Booking.User");
            ModelState.Remove("Booking.UserID");
            ModelState.Remove("Booking.Amenity");
            ModelState.Remove("Booking.AmenityID");
            ModelState.Remove("Booking.BookingID");
            ModelState.Remove("Booking.AmountPaid");
            ModelState.Remove("Booking.BookingDate");

            /*string userId = paymentMethod.Booking.UserID;
            string amenityId = paymentMethod.Booking.AmenityID;

            User user = _context.User.FindAsync(userId);
            Amenity amenity = _context.Amenity.FindAsync(amenityId);

            paymentMethod.Booking.User = user;
            paymentMethod.Booking.Amenity = amenity;*/

            // Always set the payment to success for this project
            if (ModelState.IsValid)
            {
                _context.Booking.Add(paymentMethod.Booking);
                _context.SaveChanges();
                _amenityManagementService.UpdateAmenityCapacity(paymentMethod.Booking);
                //return Ok(new { Success = true, Message = "Payment Successful !!!" });
                return RedirectToAction("Index", "Bookings");
            }

            return View("BookingPaymentForm", paymentMethod);
        }
    }
}
