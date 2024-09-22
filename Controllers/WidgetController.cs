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
    public class WidgetController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public WidgetController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: Widget
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.Widgets.Include(w => w.User);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: Widget/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var widget = await _context.Widgets
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.WidgetId == id);
            if (widget == null)
            {
                return NotFound();
            }

            return View(widget);
        }

        // GET: Widget/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Widget/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WidgetId,UserId,WidgetType,Settings")] Widget widget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(widget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", widget.UserId);
            return View(widget);
        }

        // GET: Widget/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var widget = await _context.Widgets.FindAsync(id);
            if (widget == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", widget.UserId);
            return View(widget);
        }

        // POST: Widget/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WidgetId,UserId,WidgetType,Settings")] Widget widget)
        {
            if (id != widget.WidgetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(widget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WidgetExists(widget.WidgetId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", widget.UserId);
            return View(widget);
        }

        // GET: Widget/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var widget = await _context.Widgets
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.WidgetId == id);
            if (widget == null)
            {
                return NotFound();
            }

            return View(widget);
        }

        // POST: Widget/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var widget = await _context.Widgets.FindAsync(id);
            if (widget != null)
            {
                _context.Widgets.Remove(widget);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WidgetExists(int id)
        {
            return _context.Widgets.Any(e => e.WidgetId == id);
        }
    }
}
