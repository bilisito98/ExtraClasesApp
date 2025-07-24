using ExtraClasesApp.Data;
using ExtraClasesApp.Models;
using ExtraClasesApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExtraClasesApp.Controllers
{
    public class NotificacionesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailSenderService _emailSender;

        public NotificacionesController(AppDbContext context, EmailSenderService emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            var notificaciones = await _context.Notificaciones
                .Include(n => n.Tutor)
                .ToListAsync();

            return View(notificaciones);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var notificacion = await _context.Notificaciones
                .Include(n => n.Tutor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return notificacion is null ? NotFound() : View(notificacion);
        }

        public IActionResult Create()
        {
            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Notificacion notificacion)
        {
            if (!ModelState.IsValid)
            {
                ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", notificacion.TutorId);
                return View(notificacion);
            }

            var tutor = await _context.Tutores.FindAsync(notificacion.TutorId);
            if (tutor == null)
            {
                ModelState.AddModelError("", "Tutor no encontrado.");
                ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", notificacion.TutorId);
                return View(notificacion);
            }

            // ✅ Guardar con fecha en UTC
            notificacion.FechaEnvio = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

            // Enviar correo
            var asunto = notificacion.Asunto;
            var cuerpo = $"<p><strong>{notificacion.Mensaje}</strong></p><p>Enviado el {notificacion.FechaEnvio}</p>";

            await _emailSender.EnviarCorreoAsync(tutor.Email, asunto, cuerpo);

            // Guardar en la base de datos
            _context.Add(notificacion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion is null) return NotFound();

            ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", notificacion.TutorId);
            return View(notificacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Notificacion notificacion)
        {
            if (id != notificacion.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["TutorId"] = new SelectList(_context.Tutores, "Id", "NombreCompleto", notificacion.TutorId);
                return View(notificacion);
            }

            try
            {
                // Asegurar FechaEnvio UTC si el usuario edita la fecha manualmente
                notificacion.FechaEnvio = DateTime.SpecifyKind(notificacion.FechaEnvio, DateTimeKind.Utc);

                _context.Update(notificacion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Notificaciones.Any(n => n.Id == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var notificacion = await _context.Notificaciones
                .Include(n => n.Tutor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return notificacion is null ? NotFound() : View(notificacion);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion != null)
            {
                _context.Notificaciones.Remove(notificacion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
