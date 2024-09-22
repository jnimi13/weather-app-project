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
    public class SunriseSunsetTimeController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public SunriseSunsetTimeController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: SunriseSunsetTime
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.SunriseSunsetTimes.Include(s => s.Location);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: SunriseSunsetTime/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sunriseSunsetTime = await _context.SunriseSunsetTimes
                .Include(s => s.Location)
                .FirstOrDefaultAsync(m => m.SunriseSunsetId == id);
            if (sunriseSunsetTime == null)
            {
                return NotFound();
            }

            return View(sunriseSunsetTime);
        }

        // GET: SunriseSunsetTime/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: SunriseSunsetTime/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SunriseSunsetId,LocationId,SunriseTime,SunsetTime,Date")] SunriseSunsetTime sunriseSunsetTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sunriseSunsetTime);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", sunriseSunsetTime.LocationId);
            return View(sunriseSunsetTime);
        }

        // GET: SunriseSunsetTime/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sunriseSunsetTime = await _context.SunriseSunsetTimes.FindAsync(id);
            if (sunriseSunsetTime == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", sunriseSunsetTime.LocationId);
            return View(sunriseSunsetTime);
        }

        // POST: SunriseSunsetTime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SunriseSunsetId,LocationId,SunriseTime,SunsetTime,Date")] SunriseSunsetTime sunriseSunsetTime)
        {
            if (id != sunriseSunsetTime.SunriseSunsetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sunriseSunsetTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SunriseSunsetTimeExists(sunriseSunsetTime.SunriseSunsetId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", sunriseSunsetTime.LocationId);
            return View(sunriseSunsetTime);
        }

        // GET: SunriseSunsetTime/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sunriseSunsetTime = await _context.SunriseSunsetTimes
                .Include(s => s.Location)
                .FirstOrDefaultAsync(m => m.SunriseSunsetId == id);
            if (sunriseSunsetTime == null)
            {
                return NotFound();
            }

            return View(sunriseSunsetTime);
        }

        // POST: SunriseSunsetTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sunriseSunsetTime = await _context.SunriseSunsetTimes.FindAsync(id);
            if (sunriseSunsetTime != null)
            {
                _context.SunriseSunsetTimes.Remove(sunriseSunsetTime);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SunriseSunsetTimeExists(int id)
        {
            return _context.SunriseSunsetTimes.Any(e => e.SunriseSunsetId == id);
        }
    }
}
