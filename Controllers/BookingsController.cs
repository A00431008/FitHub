using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitHub.Data;
using FitHub.Models;
using FitHub.Services;
using Newtonsoft.Json;

namespace FitHub.Controllers
{
    public class BookingsController : Controller
    {
        private readonly GymDbContext _context;
        private readonly AmenityManagementService _amenityManagementService;

        public BookingsController(GymDbContext context, AmenityManagementService amenityManagementService)
        {
            _context = context;
            _amenityManagementService = amenityManagementService;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var gymDbContext = _context.Booking.Include(b => b.Amenity).Include(b => b.User);
            return View(await gymDbContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Amenity)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["AmenityID"] = new SelectList(_context.Amenity, "AmenityID", "AmenityName");
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingID,UserID,AmenityID,BookingDate,NumberOfPeople,AmountPaid,PurchasedDate")] Booking booking)
        {
            string userId = booking.UserID;
            string amenityId = booking.AmenityID;

            var user = await _context.User.FindAsync(userId);
            var amenity = await _context.Amenity.FindAsync(amenityId);

            booking.User = user;
            booking.Amenity = amenity;
            booking.AmountPaid = booking.NumberOfPeople * amenity.CostPerPerson;
            booking.PurchasedDate = DateTime.Now;

            if (booking.NumberOfPeople <= 0)
            {
                ModelState.AddModelError(nameof(Booking.NumberOfPeople), "Number of people must be greater than zero.");
            }

            else if (!_amenityManagementService.IsBookingValid(booking))
            {
                ModelState.AddModelError(nameof(Booking.NumberOfPeople), "Booking is not valid based on capacity constraints.");
            }

            ModelState.Remove("BookingID");
            ModelState.Remove("User");
            ModelState.Remove("Amenity");

            if (ModelState.IsValid)
            {
                /*_context.Add(booking);
                await _context.SaveChangesAsync();
                _amenityManagementService.UpdateAmenityCapacity(booking);
                return RedirectToAction(nameof(Index));*/
                var bookingJson = JsonConvert.SerializeObject(booking);
                TempData["BookingData"] = bookingJson;
                //TempData["BookingData"] = booking;
                return RedirectToAction("PaymentForm", "Payment");
            }
            ViewData["AmenityID"] = new SelectList(_context.Amenity, "AmenityID", "AmenityName", booking.AmenityID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", booking.UserID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["AmenityID"] = new SelectList(_context.Amenity, "AmenityID", "AmenityName", booking.AmenityID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", booking.UserID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("BookingID,UserID,AmenityID,BookingDate,NumberOfPeople,AmountPaid,PurchasedDate")] Booking booking)
        {
            if (id != booking.BookingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AmenityID"] = new SelectList(_context.Amenity, "AmenityID", "AmenityName", booking.AmenityID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", booking.UserID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.Amenity)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BookingID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Booking == null)
            {
                return Problem("Entity set 'GymDbContext.Booking'  is null.");
            }
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(string id)
        {
          return (_context.Booking?.Any(e => e.BookingID == id)).GetValueOrDefault();
        }
    }
}
