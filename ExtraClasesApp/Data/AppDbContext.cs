using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExtraClasesApp.Models;

namespace ExtraClasesApp.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tutor> Tutores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<ClaseExtra> ClasesExtras { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<PreferenciasTutor> PreferenciasTutores { get; set; }
        public DbSet<MensajeEnviado> MensajesEnviados { get; set; }

        // Puedes personalizar más configuraciones aquí si necesitas
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Aquí puedes agregar reglas de configuración si usas Fluent API.
            // Ejemplo:
            // builder.Entity<Estudiante>().HasIndex(e => e.EmailPadre).IsUnique();
        }
    }
}
