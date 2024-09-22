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
    public class WeatherDatumController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public WeatherDatumController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: WeatherDatum
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.WeatherData.Include(w => w.Location);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: WeatherDatum/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherDatum = await _context.WeatherData
                .Include(w => w.Location)
                .FirstOrDefaultAsync(m => m.WeatherId == id);
            if (weatherDatum == null)
            {
                return NotFound();
            }

            return View(weatherDatum);
        }

        // GET: WeatherDatum/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: WeatherDatum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeatherId,LocationId,Temperature,WeatherIcon,Humidity,WindSpeed,UvIndex,RecordedAt")] WeatherDatum weatherDatum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weatherDatum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", weatherDatum.LocationId);
            return View(weatherDatum);
        }

        // GET: WeatherDatum/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherDatum = await _context.WeatherData.FindAsync(id);
            if (weatherDatum == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", weatherDatum.LocationId);
            return View(weatherDatum);
        }

        // POST: WeatherDatum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeatherId,LocationId,Temperature,WeatherIcon,Humidity,WindSpeed,UvIndex,RecordedAt")] WeatherDatum weatherDatum)
        {
            if (id != weatherDatum.WeatherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weatherDatum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherDatumExists(weatherDatum.WeatherId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", weatherDatum.LocationId);
            return View(weatherDatum);
        }

        // GET: WeatherDatum/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherDatum = await _context.WeatherData
                .Include(w => w.Location)
                .FirstOrDefaultAsync(m => m.WeatherId == id);
            if (weatherDatum == null)
            {
                return NotFound();
            }

            return View(weatherDatum);
        }

        // POST: WeatherDatum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weatherDatum = await _context.WeatherData.FindAsync(id);
            if (weatherDatum != null)
            {
                _context.WeatherData.Remove(weatherDatum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherDatumExists(int id)
        {
            return _context.WeatherData.Any(e => e.WeatherId == id);
        }
    }
}
