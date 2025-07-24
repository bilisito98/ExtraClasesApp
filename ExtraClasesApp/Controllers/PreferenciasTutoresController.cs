using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExtraClasesApp.Data;
using ExtraClasesApp.Models;

namespace ExtraClasesApp.Controllers
{
    public class PreferenciasTutoresController : Controller
    {
        private readonly AppDbContext _context;

        public PreferenciasTutoresController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var preferencias = await _context.PreferenciasTutores.Include(p => p.Tutor).ToListAsync();
            return View(preferencias);
        }

        public IActionResult Create()
        {
            ViewData["TutorId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Tutores, "Id", "NombreCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreferenciasTutor preferencias)
        {
            if (ModelState.IsValid)
            {
                _context.PreferenciasTutores.Add(preferencias);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preferencias);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pref = await _context.PreferenciasTutores.Include(p => p.Tutor).FirstOrDefaultAsync(p => p.Id == id);
            if (pref == null) return NotFound();

            return View(pref);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pref = await _context.PreferenciasTutores.FindAsync(id);
            if (pref != null)
            {
                _context.PreferenciasTutores.Remove(pref);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
