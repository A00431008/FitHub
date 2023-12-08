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
    public class MembershipsController : Controller
    {
        private readonly GymDbContext _context;
        private readonly MembershipManagementService _membMgmtService;

        public MembershipsController(GymDbContext context, MembershipManagementService membMgmtService)
        {
            _context = context;
            _membMgmtService = membMgmtService;
        }

        // GET: Memberships
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst("UserID").Value;
            var memb = _context.Membership
                        .Include(m => m.MD).Include(m => m.User)
                        .Where(m => m.UserID == userId)
                        .OrderBy(m => m.StartDate);
            return View(await memb.ToListAsync());
        }
       //public async Task<IActionResult> Index()
       //{
       //    var gymDbContext = _context.Membership.Include(m => m.MD).Include(m => m.User);
       //    return View(await gymDbContext.ToListAsync());
       //}

        // GET: Memberships/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership
                .Include(m => m.MD)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MembershipID == id);
            if (membership == null)
            {
                return NotFound();
            }

            return View(membership);
        }

        // GET: Memberships/Create
        public IActionResult Create()
        {
            ViewData["MembershipTypeID"] = new SelectList(_context.Set<MembershipDetail>(), "MembershipTypeID", "MembershipTypeName");
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID");
            return View();
        }

        // POST: Memberships/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembershipID,UserID,MembershipType,StartDate,EndDate,AmountPaid,MembershipTypeID")] Membership membership)
        {
            var claims = User.Claims;
            var userID = User.FindFirst("UserID")?.Value;
            
            string membTypeId = membership.MembershipTypeID;

            var user = await _context.User.FindAsync(userID);
            membership.UserID = user.UserID;
            var membDetail = await _context.MembershipDetail.FindAsync(membTypeId);
           
            
            membership.User = user;
            membership.MD = membDetail;

            membership.AmountPaid = membDetail.Cost;
            membership.EndDate = _membMgmtService.GetMembershipEndDate(membTypeId, membership.StartDate);

            ModelState.Remove("MD");
            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("MembershipID");

            if (ModelState.IsValid)
            {
                var membershipJson = JsonConvert.SerializeObject(membership);
                TempData["MembershipData"] = membershipJson;
                return RedirectToAction("MembershipPaymentForm", "Payment");
            }
            ViewData["MembershipTypeID"] = new SelectList(_context.Set<MembershipDetail>(), "MembershipTypeID", "MembershipTypeName", membership.MembershipTypeID);
            ViewData["UserID"] = new SelectList(_context.User, "UserID", "UserID", membership.UserID);
            return View(membership);
        }

        private bool MembershipExists(string id)
        {
            return (_context.Membership?.Any(e => e.MembershipID == id)).GetValueOrDefault();
        }
    }
}
