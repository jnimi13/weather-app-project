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
    public class DailyForecastController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public DailyForecastController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: DailyForecast
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.DailyForecasts.Include(d => d.Location);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: DailyForecast/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyForecast = await _context.DailyForecasts
                .Include(d => d.Location)
                .FirstOrDefaultAsync(m => m.DailyId == id);
            if (dailyForecast == null)
            {
                return NotFound();
            }

            return View(dailyForecast);
        }

        // GET: DailyForecast/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: DailyForecast/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DailyId,LocationId,Temperature,Precipitation,WindSpeed,WeatherIcon,ForecastDate")] DailyForecast dailyForecast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyForecast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", dailyForecast.LocationId);
            return View(dailyForecast);
        }

        // GET: DailyForecast/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyForecast = await _context.DailyForecasts.FindAsync(id);
            if (dailyForecast == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", dailyForecast.LocationId);
            return View(dailyForecast);
        }

        // POST: DailyForecast/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DailyId,LocationId,Temperature,Precipitation,WindSpeed,WeatherIcon,ForecastDate")] DailyForecast dailyForecast)
        {
            if (id != dailyForecast.DailyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyForecast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyForecastExists(dailyForecast.DailyId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", dailyForecast.LocationId);
            return View(dailyForecast);
        }

        // GET: DailyForecast/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyForecast = await _context.DailyForecasts
                .Include(d => d.Location)
                .FirstOrDefaultAsync(m => m.DailyId == id);
            if (dailyForecast == null)
            {
                return NotFound();
            }

            return View(dailyForecast);
        }

        // POST: DailyForecast/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyForecast = await _context.DailyForecasts.FindAsync(id);
            if (dailyForecast != null)
            {
                _context.DailyForecasts.Remove(dailyForecast);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyForecastExists(int id)
        {
            return _context.DailyForecasts.Any(e => e.DailyId == id);
        }
    }
}
