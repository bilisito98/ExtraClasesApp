using ExtraClasesApp.Data;
using ExtraClasesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExtraClasesApp.Controllers
{
    public class TutoresController : Controller
    {
        private readonly AppDbContext _context;

        public TutoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tutores
        public async Task<IActionResult> Index()
        {
            var tutores = await _context.Tutores.ToListAsync();
            return View(tutores);
        }

        // GET: Tutores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tutor = await _context.Tutores
                .FirstOrDefaultAsync(t => t.Id == id);

            return tutor == null ? NotFound() : View(tutor);
        }

        // GET: Tutores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tutores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tutor tutor)
        {
            if (!ModelState.IsValid)
                return View(tutor);

            _context.Tutores.Add(tutor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Tutores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tutor = await _context.Tutores.FindAsync(id);
            return tutor == null ? NotFound() : View(tutor);
        }

        // POST: Tutores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tutor tutor)
        {
            if (id != tutor.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(tutor);

            try
            {
                _context.Update(tutor);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorExists(tutor.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Tutores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tutor = await _context.Tutores
                .FirstOrDefaultAsync(t => t.Id == id);

            return tutor == null ? NotFound() : View(tutor);
        }

        // POST: Tutores/DeleteConfirmed
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutor = await _context.Tutores.FindAsync(id);
            if (tutor != null)
            {
                _context.Tutores.Remove(tutor);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TutorExists(int id)
        {
            return _context.Tutores.Any(t => t.Id == id);
        }
    }
}
