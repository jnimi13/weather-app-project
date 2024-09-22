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
    public class HourlyForecastController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public HourlyForecastController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: HourlyForecast
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.HourlyForecasts.Include(h => h.Location);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: HourlyForecast/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hourlyForecast = await _context.HourlyForecasts
                .Include(h => h.Location)
                .FirstOrDefaultAsync(m => m.HourlyId == id);
            if (hourlyForecast == null)
            {
                return NotFound();
            }

            return View(hourlyForecast);
        }

        // GET: HourlyForecast/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: HourlyForecast/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HourlyId,LocationId,Temperature,Precipitation,WindSpeed,WeatherIcon,ForecastTime")] HourlyForecast hourlyForecast)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hourlyForecast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", hourlyForecast.LocationId);
            return View(hourlyForecast);
        }

        // GET: HourlyForecast/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hourlyForecast = await _context.HourlyForecasts.FindAsync(id);
            if (hourlyForecast == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", hourlyForecast.LocationId);
            return View(hourlyForecast);
        }

        // POST: HourlyForecast/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HourlyId,LocationId,Temperature,Precipitation,WindSpeed,WeatherIcon,ForecastTime")] HourlyForecast hourlyForecast)
        {
            if (id != hourlyForecast.HourlyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hourlyForecast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HourlyForecastExists(hourlyForecast.HourlyId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", hourlyForecast.LocationId);
            return View(hourlyForecast);
        }

        // GET: HourlyForecast/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hourlyForecast = await _context.HourlyForecasts
                .Include(h => h.Location)
                .FirstOrDefaultAsync(m => m.HourlyId == id);
            if (hourlyForecast == null)
            {
                return NotFound();
            }

            return View(hourlyForecast);
        }

        // POST: HourlyForecast/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hourlyForecast = await _context.HourlyForecasts.FindAsync(id);
            if (hourlyForecast != null)
            {
                _context.HourlyForecasts.Remove(hourlyForecast);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HourlyForecastExists(int id)
        {
            return _context.HourlyForecasts.Any(e => e.HourlyId == id);
        }
    }
}
