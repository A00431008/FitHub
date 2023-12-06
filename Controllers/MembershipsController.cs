using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitHub.Data;
using FitHub.Models;

namespace FitHub.Controllers
{
    public class MembershipsController : Controller
    {
        private readonly MembershipDBContext _context;

        public MembershipsController(MembershipDBContext context)
        {
            _context = context;
        }

        // GET: Memberships
        public async Task<IActionResult> Index()
        {
            var membershipDBContext = _context.Membership.Include(m => m.MD).Include(m => m.User);
            return View(await membershipDBContext.ToListAsync());
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
            ViewData["MembershipTypeID"] = new SelectList(_context.Set<MembershipDetail>(), "MembershipTypeID", "MembershipTypeID");
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "UserID");
            return View();
        }

        // POST: Memberships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembershipID,UserID,MembershipType,StartDate,EndDate,AmountPaid,MembershipTypeID")] Membership membership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MembershipTypeID"] = new SelectList(_context.Set<MembershipDetail>(), "MembershipTypeID", "MembershipTypeID", membership.MembershipTypeID);
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "UserID", membership.UserID);
            return View(membership);
        }

        // GET: Memberships/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Membership == null)
            {
                return NotFound();
            }

            var membership = await _context.Membership.FindAsync(id);
            if (membership == null)
            {
                return NotFound();
            }
            ViewData["MembershipTypeID"] = new SelectList(_context.Set<MembershipDetail>(), "MembershipTypeID", "MembershipTypeID", membership.MembershipTypeID);
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "UserID", membership.UserID);
            return View(membership);
        }

        // POST: Memberships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MembershipID,UserID,MembershipType,StartDate,EndDate,AmountPaid,MembershipTypeID")] Membership membership)
        {
            if (id != membership.MembershipID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipExists(membership.MembershipID))
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
            ViewData["MembershipTypeID"] = new SelectList(_context.Set<MembershipDetail>(), "MembershipTypeID", "MembershipTypeID", membership.MembershipTypeID);
            ViewData["UserID"] = new SelectList(_context.Set<User>(), "UserID", "UserID", membership.UserID);
            return View(membership);
        }

        // GET: Memberships/Delete/5
        public async Task<IActionResult> Delete(string id)
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

        // POST: Memberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Membership == null)
            {
                return Problem("Entity set 'MembershipDBContext.Membership'  is null.");
            }
            var membership = await _context.Membership.FindAsync(id);
            if (membership != null)
            {
                _context.Membership.Remove(membership);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipExists(string id)
        {
          return (_context.Membership?.Any(e => e.MembershipID == id)).GetValueOrDefault();
        }
    }
}
