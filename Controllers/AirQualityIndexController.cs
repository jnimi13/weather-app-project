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
    public class AirQualityIndexController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public AirQualityIndexController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: AirQualityIndex
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.AirQualityIndices.Include(a => a.Location);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: AirQualityIndex/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airQualityIndex = await _context.AirQualityIndices
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.AqiId == id);
            if (airQualityIndex == null)
            {
                return NotFound();
            }

            return View(airQualityIndex);
        }

        // GET: AirQualityIndex/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: AirQualityIndex/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AqiId,LocationId,Aqi,Pollutants,RecordedAt")] AirQualityIndex airQualityIndex)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airQualityIndex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", airQualityIndex.LocationId);
            return View(airQualityIndex);
        }

        // GET: AirQualityIndex/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airQualityIndex = await _context.AirQualityIndices.FindAsync(id);
            if (airQualityIndex == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", airQualityIndex.LocationId);
            return View(airQualityIndex);
        }

        // POST: AirQualityIndex/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AqiId,LocationId,Aqi,Pollutants,RecordedAt")] AirQualityIndex airQualityIndex)
        {
            if (id != airQualityIndex.AqiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airQualityIndex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirQualityIndexExists(airQualityIndex.AqiId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", airQualityIndex.LocationId);
            return View(airQualityIndex);
        }

        // GET: AirQualityIndex/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airQualityIndex = await _context.AirQualityIndices
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.AqiId == id);
            if (airQualityIndex == null)
            {
                return NotFound();
            }

            return View(airQualityIndex);
        }

        // POST: AirQualityIndex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airQualityIndex = await _context.AirQualityIndices.FindAsync(id);
            if (airQualityIndex != null)
            {
                _context.AirQualityIndices.Remove(airQualityIndex);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirQualityIndexExists(int id)
        {
            return _context.AirQualityIndices.Any(e => e.AqiId == id);
        }
    }
}
