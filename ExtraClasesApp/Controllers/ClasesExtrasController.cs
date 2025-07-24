using ExtraClasesApp.Data;
using ExtraClasesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExtraClasesApp.Controllers
{
    public class ClasesExtrasController : Controller
    {
        private readonly AppDbContext _context;

        public ClasesExtrasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var clases = await _context.ClasesExtras
                .Include(c => c.Tutor)
                .Include(c => c.Estudiante)
                .ToListAsync();

            return View(clases);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var clase = await _context.ClasesExtras
                .Include(c => c.Tutor)
                .Include(c => c.Estudiante)
                .FirstOrDefaultAsync(c => c.Id == id);

            return clase == null ? NotFound() : View(clase);
        }

        public IActionResult Create()
        {
            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "NombreCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClaseExtra claseExtra)
        {
            if (!ModelState.IsValid)
            {
                ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", claseExtra.TutorId);
                ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "NombreCompleto", claseExtra.EstudianteId);
                return View(claseExtra);
            }

            // ✅ Forzar UTC antes de guardar
            claseExtra.FechaHoraTutor = DateTime.SpecifyKind(claseExtra.FechaHoraTutor, DateTimeKind.Utc);
            claseExtra.FechaHoraEstudiante = DateTime.SpecifyKind(claseExtra.FechaHoraEstudiante, DateTimeKind.Utc);

            _context.Add(claseExtra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var clase = await _context.ClasesExtras.FindAsync(id);
            if (clase == null) return NotFound();

            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", clase.TutorId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "NombreCompleto", clase.EstudianteId);
            return View(clase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClaseExtra claseExtra)
        {
            if (id != claseExtra.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", claseExtra.TutorId);
                ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "NombreCompleto", claseExtra.EstudianteId);
                return View(claseExtra);
            }

            try
            {
                claseExtra.FechaHoraTutor = DateTime.SpecifyKind(claseExtra.FechaHoraTutor, DateTimeKind.Utc);
                claseExtra.FechaHoraEstudiante = DateTime.SpecifyKind(claseExtra.FechaHoraEstudiante, DateTimeKind.Utc);

                _context.Update(claseExtra);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaseExtraExists(claseExtra.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var clase = await _context.ClasesExtras
                .Include(c => c.Tutor)
                .Include(c => c.Estudiante)
                .FirstOrDefaultAsync(c => c.Id == id);

            return clase == null ? NotFound() : View(clase);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clase = await _context.ClasesExtras.FindAsync(id);
            if (clase != null)
            {
                _context.ClasesExtras.Remove(clase);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClaseExtraExists(int id)
        {
            return _context.ClasesExtras.Any(e => e.Id == id);
        }
    }
}
