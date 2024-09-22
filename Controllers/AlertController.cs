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
    public class AlertController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public AlertController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: Alert
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.Alerts.Include(a => a.Location);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: Alert/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.AlertId == id);
            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }

        // GET: Alert/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: Alert/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlertId,LocationId,AlertType,Message,IssuedAt")] Alert alert)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alert);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", alert.LocationId);
            return View(alert);
        }

        // GET: Alert/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts.FindAsync(id);
            if (alert == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", alert.LocationId);
            return View(alert);
        }

        // POST: Alert/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlertId,LocationId,AlertType,Message,IssuedAt")] Alert alert)
        {
            if (id != alert.AlertId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alert);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlertExists(alert.AlertId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", alert.LocationId);
            return View(alert);
        }

        // GET: Alert/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alert = await _context.Alerts
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.AlertId == id);
            if (alert == null)
            {
                return NotFound();
            }

            return View(alert);
        }

        // POST: Alert/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alert = await _context.Alerts.FindAsync(id);
            if (alert != null)
            {
                _context.Alerts.Remove(alert);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlertExists(int id)
        {
            return _context.Alerts.Any(e => e.AlertId == id);
        }
    }
}
