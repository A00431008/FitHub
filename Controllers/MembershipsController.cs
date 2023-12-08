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
            var gymDbContext = _context.Membership.Include(m => m.MD).Include(m => m.User);
            return View(await gymDbContext.ToListAsync());
        }

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
            string emailFromCookies = User.FindFirst("UserID")?.Value;

            
            string membTypeId = membership.MembershipTypeID;

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == emailFromCookies);
            string userId = user.UserID;
            //var user = await _context.User.FindAsync(userId);
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
                _context.Add(membership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
