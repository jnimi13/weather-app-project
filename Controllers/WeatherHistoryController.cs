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
    public class WeatherHistoryController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public WeatherHistoryController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: WeatherHistory
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.WeatherHistories.Include(w => w.Location);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: WeatherHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherHistory = await _context.WeatherHistories
                .Include(w => w.Location)
                .FirstOrDefaultAsync(m => m.HistoryId == id);
            if (weatherHistory == null)
            {
                return NotFound();
            }

            return View(weatherHistory);
        }

        // GET: WeatherHistory/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId");
            return View();
        }

        // POST: WeatherHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoryId,LocationId,Temperature,Precipitation,WindSpeed,WeatherIcon,RecordedDate")] WeatherHistory weatherHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weatherHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", weatherHistory.LocationId);
            return View(weatherHistory);
        }

        // GET: WeatherHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherHistory = await _context.WeatherHistories.FindAsync(id);
            if (weatherHistory == null)
            {
                return NotFound();
            }
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", weatherHistory.LocationId);
            return View(weatherHistory);
        }

        // POST: WeatherHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoryId,LocationId,Temperature,Precipitation,WindSpeed,WeatherIcon,RecordedDate")] WeatherHistory weatherHistory)
        {
            if (id != weatherHistory.HistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weatherHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherHistoryExists(weatherHistory.HistoryId))
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
            ViewData["LocationId"] = new SelectList(_context.Locations, "LocationId", "LocationId", weatherHistory.LocationId);
            return View(weatherHistory);
        }

        // GET: WeatherHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherHistory = await _context.WeatherHistories
                .Include(w => w.Location)
                .FirstOrDefaultAsync(m => m.HistoryId == id);
            if (weatherHistory == null)
            {
                return NotFound();
            }

            return View(weatherHistory);
        }

        // POST: WeatherHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weatherHistory = await _context.WeatherHistories.FindAsync(id);
            if (weatherHistory != null)
            {
                _context.WeatherHistories.Remove(weatherHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherHistoryExists(int id)
        {
            return _context.WeatherHistories.Any(e => e.HistoryId == id);
        }
    }
}
