using ExtraClasesApp.Data;
using ExtraClasesApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExtraClasesApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? desde, DateTime? hasta)
        {
            var inicio = (desde ?? DateTime.Today.AddDays(-6)).Date.ToUniversalTime();
            var fin = (hasta ?? DateTime.Today).Date.ToUniversalTime().AddDays(1).AddTicks(-1); // Fin del día

            var totalTutores = await _context.Tutores.CountAsync();
            var totalEstudiantes = await _context.Estudiantes.CountAsync();
            var totalClases = await _context.ClasesExtras
                .Where(c => c.FechaHoraTutor >= inicio && c.FechaHoraTutor <= fin)
                .CountAsync();
            var totalNotificaciones = await _context.Notificaciones
                .Where(n => n.FechaEnvio >= inicio && n.FechaEnvio <= fin)
                .CountAsync();

            var dias = Enumerable.Range(0, (fin - inicio).Days + 1)
                .Select(i => inicio.AddDays(i).Date)
                .ToList();

            var clasesPorDia = await _context.ClasesExtras
                .Where(c => c.FechaHoraTutor >= inicio && c.FechaHoraTutor <= fin)
                .GroupBy(c => c.FechaHoraTutor.Date)
                .Select(g => new { Fecha = g.Key, Count = g.Count() })
                .ToListAsync();

            var notisPorDia = await _context.Notificaciones
                .Where(n => n.FechaEnvio >= inicio && n.FechaEnvio <= fin)
                .GroupBy(n => n.FechaEnvio.Date)
                .Select(g => new { Fecha = g.Key, Count = g.Count() })
                .ToListAsync();

            var model = new DashboardViewModel
            {
                TotalTutores = totalTutores,
                TotalEstudiantes = totalEstudiantes,
                TotalClases = totalClases,
                TotalNotificaciones = totalNotificaciones,
                ChartLabels = dias.Select(d => d.ToString("yyyy-MM-dd")).ToList(),
                ChartClases = dias.Select(d => clasesPorDia.FirstOrDefault(x => x.Fecha == d)?.Count ?? 0).ToList(),
                ChartNotis = dias.Select(d => notisPorDia.FirstOrDefault(x => x.Fecha == d)?.Count ?? 0).ToList(),
                Desde = inicio.ToLocalTime().ToString("yyyy-MM-dd"),
                Hasta = fin.ToLocalTime().ToString("yyyy-MM-dd")
            };

            return View(model);
        }
    }
}
