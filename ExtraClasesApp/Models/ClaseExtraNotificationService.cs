using ExtraClasesApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

public class ClaseExtraNotificationService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public ClaseExtraNotificationService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var ahora = DateTime.Now;
            var unaHoraDespues = ahora.AddHours(1);

            var clases = await context.ClasesExtras
                .Include(c => c.Tutor)
                .Include(c => c.Estudiante)
                .Where(c => c.FechaHoraTutor >= ahora && c.FechaHoraTutor <= unaHoraDespues && !c.Notificado)
                .ToListAsync();

            foreach (var clase in clases)
            {
                // 📨 Lógica de notificación (aquí ejemplo de correo simple)
                var destinatario = clase.Tutor?.Email ?? "default@correo.com";
                var mensaje = $"Recordatorio: tienes una clase de '{clase.Modulo}' con {clase.Estudiante?.NombreCompleto} a las {clase.FechaHoraTutor:HH:mm}.";

                EnviarCorreo(destinatario, "Recordatorio de Clase Extra", mensaje);

                clase.Notificado = true;
                context.ClasesExtras.Update(clase);
            }

            await context.SaveChangesAsync();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private void EnviarCorreo(string destinatario, string asunto, string cuerpo)
    {
        var mail = new MailMessage("tuservidor@correo.com", destinatario, asunto, cuerpo);
        var smtp = new SmtpClient("smtp.tuservidor.com")
        {
            Port = 587,
            Credentials = new System.Net.NetworkCredential("usuario", "contraseña"),
            EnableSsl = true
        };
        smtp.Send(mail);
    }
}
