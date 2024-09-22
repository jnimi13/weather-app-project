using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class ProfileManagementController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public ProfileManagementController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: ProfileManagement
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.ProfileManagements.Include(p => p.User);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: ProfileManagement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileManagement = await _context.ProfileManagements
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProfileId == id);
            if (profileManagement == null)
            {
                return NotFound();
            }

            return View(profileManagement);
        }

        // GET: ProfileManagement/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: ProfileManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileId,UserId,ProfileData")] ProfileManagement profileManagement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profileManagement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", profileManagement.UserId);
            return View(profileManagement);
        }

        // GET: ProfileManagement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileManagement = await _context.ProfileManagements.FindAsync(id);
            if (profileManagement == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", profileManagement.UserId);
            return View(profileManagement);
        }

        // POST: ProfileManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileId,UserId,ProfileData")] ProfileManagement profileManagement)
        {
            if (id != profileManagement.ProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profileManagement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileManagementExists(profileManagement.ProfileId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", profileManagement.UserId);
            return View(profileManagement);
        }

        // GET: ProfileManagement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileManagement = await _context.ProfileManagements
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProfileId == id);
            if (profileManagement == null)
            {
                return NotFound();
            }

            return View(profileManagement);
        }

        // POST: ProfileManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profileManagement = await _context.ProfileManagements.FindAsync(id);
            if (profileManagement != null)
            {
                _context.ProfileManagements.Remove(profileManagement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileManagementExists(int id)
        {
            return _context.ProfileManagements.Any(e => e.ProfileId == id);
        }
    }
}
