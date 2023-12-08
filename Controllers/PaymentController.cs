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
        public IActionResult MembershipPaymentForm()
        {
            var membership = JsonConvert.DeserializeObject<Membership>(TempData["MembershipData"] as String);

            if (membership == null)
            {
                return NotFound();
            }

            return View("MembershipPaymentForm", new PaymentMethod { Membership = membership });
            /*// Declare and return an empty payment method form at the beginning
            var paymentMethod = new PaymentMethod();
            return View(paymentMethod);*/
        }

        [HttpPost]
        /*[Route("/api/payment/process")]*/
        public IActionResult ProcessMembershipPayment(PaymentMethod paymentMethod)
        {

            ModelState.Remove("Membership.MembershipID");
            ModelState.Remove("Membership.UserID");
            ModelState.Remove("Membership.MembershipTypeID");
            ModelState.Remove("Membership.StartDate");
            ModelState.Remove("Membership.EndDate");
            ModelState.Remove("Membership.AmountPaid");
            ModelState.Remove("Membership.MD");
            ModelState.Remove("Membership.User");

            // Always set the payment to success for this project
            if (ModelState.IsValid)
            {
                _context.Membership.Add(paymentMethod.Membership);
                _context.SaveChanges();
                return RedirectToAction("Index", "Memberships");
            }

            return View("MembershipPaymentForm", paymentMethod);
        }
    }
}
