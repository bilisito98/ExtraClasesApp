using ExtraClasesApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace ExtraClasesApp.Services
{
    public class ClaseExtraNotificationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ClaseExtraNotificationService> _logger;
        private readonly EmailSettings _emailSettings;

        public ClaseExtraNotificationService(
            IServiceScopeFactory scopeFactory,
            ILogger<ClaseExtraNotificationService> logger,
            IOptions<EmailSettings> emailOptions)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _emailSettings = emailOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var ahora = DateTime.UtcNow;
                    var unaHoraDespues = ahora.AddHours(1);

                    var clasesPendientes = await context.ClasesExtras
                        .Include(c => c.Tutor)
                        .Include(c => c.Estudiante)
                        .Where(c => !c.Notificado &&
                                    c.FechaHoraTutor >= ahora &&
                                    c.FechaHoraTutor <= unaHoraDespues)
                        .ToListAsync(stoppingToken);

                    foreach (var clase in clasesPendientes)
                    {
                        await NotificarClase(clase, context, stoppingToken);
                    }

                    await context.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Error general en el servicio de notificaciones.");
                }

                // Espera 10 minutos antes de volver a verificar
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        private async Task NotificarClase(Models.ClaseExtra clase, AppDbContext context, CancellationToken cancellationToken)
        {
            try
            {
                // 🧑‍🏫 Notificación al tutor
                if (!string.IsNullOrWhiteSpace(clase.Tutor?.Email))
                {
                    var mensajeTutor = $"Hola {clase.Tutor.NombreCompleto},\n\n" +
                                       $"Tienes una clase programada hoy a las {clase.FechaHoraTutor:HH:mm}.\n" +
                                       $"Módulo: {clase.Modulo}\nLección: {clase.Leccion}\n\n" +
                                       $"¡Buena clase!\nExtraClasesApp";

                    await EnviarCorreo(clase.Tutor.Email, "📘 Recordatorio de Clase Extra", mensajeTutor);
                }

                // 👨‍👩‍👧 Notificación al padre/madre del estudiante
                if (!string.IsNullOrWhiteSpace(clase.Estudiante?.EmailPadre))
                {
                    var mensajePadre = $"Hola,\n\n" +
                                       $"El estudiante {clase.Estudiante.NombreCompleto} tiene una clase hoy a las {clase.FechaHoraEstudiante:HH:mm}.\n" +
                                       $"Módulo: {clase.Modulo}\n\n" +
                                       $"Gracias por confiar en nosotros,\nExtraClasesApp";

                    await EnviarCorreo(clase.Estudiante.EmailPadre, "👨‍🎓 Recordatorio para el representante", mensajePadre);
                }

                // ✅ Marcar como notificado
                clase.Notificado = true;
                context.ClasesExtras.Update(clase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error al enviar notificación para la clase del tutor '{Tutor}'", clase.Tutor?.NombreCompleto ?? "Desconocido");
            }
        }

        private async Task EnviarCorreo(string destinatario, string asunto, string mensaje)
        {
            var remitente = new MailAddress(_emailSettings.FromEmail, _emailSettings.DisplayName);
            var receptor = new MailAddress(destinatario);

            using var smtp = new SmtpClient
            {
                Host = _emailSettings.SmtpHost,
                Port = _emailSettings.SmtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSettings.SmtpUser, _emailSettings.SmtpPass)
            };

            using var mail = new MailMessage(remitente, receptor)
            {
                Subject = asunto,
                Body = mensaje
            };

            await smtp.SendMailAsync(mail);
        }
    }
}
