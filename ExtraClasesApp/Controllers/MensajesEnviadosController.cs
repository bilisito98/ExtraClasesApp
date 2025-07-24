using ExtraClasesApp.Data;
using ExtraClasesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExtraClasesApp.Controllers
{
    public class MensajesEnviadosController : Controller
    {
        private readonly AppDbContext _context;

        public MensajesEnviadosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var mensajes = _context.MensajesEnviados
                .Include(m => m.Tutor)
                .Include(m => m.ClaseExtra);
            return View(await mensajes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var mensaje = await _context.MensajesEnviados
                .Include(m => m.Tutor)
                .Include(m => m.ClaseExtra)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mensaje == null) return NotFound();

            return View(mensaje);
        }

        public IActionResult Create()
        {
            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto");
            ViewData["ClaseExtraId"] = new SelectList(_context.ClasesExtras, "Id", "Modulo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MensajeEnviado mensaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mensaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", mensaje.TutorId);
            ViewData["ClaseExtraId"] = new SelectList(_context.ClasesExtras, "Id", "Modulo", mensaje.ClaseExtraId);
            return View(mensaje);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mensaje = await _context.MensajesEnviados.FindAsync(id);
            if (mensaje == null) return NotFound();

            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", mensaje.TutorId);
            ViewData["ClaseExtraId"] = new SelectList(_context.ClasesExtras, "Id", "Modulo", mensaje.ClaseExtraId);
            return View(mensaje);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MensajeEnviado mensaje)
        {
            if (id != mensaje.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mensaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MensajesEnviados.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", mensaje.TutorId);
            ViewData["ClaseExtraId"] = new SelectList(_context.ClasesExtras, "Id", "Modulo", mensaje.ClaseExtraId);
            return View(mensaje);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var mensaje = await _context.MensajesEnviados
                .Include(m => m.Tutor)
                .Include(m => m.ClaseExtra)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mensaje == null) return NotFound();

            return View(mensaje);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mensaje = await _context.MensajesEnviados.FindAsync(id);
            if (mensaje != null)
            {
                _context.MensajesEnviados.Remove(mensaje);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
