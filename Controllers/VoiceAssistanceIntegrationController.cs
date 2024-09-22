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
    public class VoiceAssistanceIntegrationController : Controller
    {
        private readonly WeatherAppDbContext _context;

        public VoiceAssistanceIntegrationController(WeatherAppDbContext context)
        {
            _context = context;
        }

        // GET: VoiceAssistanceIntegration
        public async Task<IActionResult> Index()
        {
            var weatherAppDbContext = _context.VoiceAssistanceIntegrations.Include(v => v.User);
            return View(await weatherAppDbContext.ToListAsync());
        }

        // GET: VoiceAssistanceIntegration/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiceAssistanceIntegration = await _context.VoiceAssistanceIntegrations
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VoiceId == id);
            if (voiceAssistanceIntegration == null)
            {
                return NotFound();
            }

            return View(voiceAssistanceIntegration);
        }

        // GET: VoiceAssistanceIntegration/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: VoiceAssistanceIntegration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoiceId,UserId,IntegrationType,Settings")] VoiceAssistanceIntegration voiceAssistanceIntegration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(voiceAssistanceIntegration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", voiceAssistanceIntegration.UserId);
            return View(voiceAssistanceIntegration);
        }

        // GET: VoiceAssistanceIntegration/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiceAssistanceIntegration = await _context.VoiceAssistanceIntegrations.FindAsync(id);
            if (voiceAssistanceIntegration == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", voiceAssistanceIntegration.UserId);
            return View(voiceAssistanceIntegration);
        }

        // POST: VoiceAssistanceIntegration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoiceId,UserId,IntegrationType,Settings")] VoiceAssistanceIntegration voiceAssistanceIntegration)
        {
            if (id != voiceAssistanceIntegration.VoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voiceAssistanceIntegration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoiceAssistanceIntegrationExists(voiceAssistanceIntegration.VoiceId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", voiceAssistanceIntegration.UserId);
            return View(voiceAssistanceIntegration);
        }

        // GET: VoiceAssistanceIntegration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiceAssistanceIntegration = await _context.VoiceAssistanceIntegrations
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.VoiceId == id);
            if (voiceAssistanceIntegration == null)
            {
                return NotFound();
            }

            return View(voiceAssistanceIntegration);
        }

        // POST: VoiceAssistanceIntegration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voiceAssistanceIntegration = await _context.VoiceAssistanceIntegrations.FindAsync(id);
            if (voiceAssistanceIntegration != null)
            {
                _context.VoiceAssistanceIntegrations.Remove(voiceAssistanceIntegration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoiceAssistanceIntegrationExists(int id)
        {
            return _context.VoiceAssistanceIntegrations.Any(e => e.VoiceId == id);
        }
    }
}
