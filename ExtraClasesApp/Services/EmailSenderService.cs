// Services/EmailSenderService.cs
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace ExtraClasesApp.Services
{
    public class EmailSenderService
    {
        private readonly EmailSettings _settings;

        public EmailSenderService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> EnviarCorreoAsync(string destino, string asunto, string cuerpo)
        {
            try
            {
                var mensaje = new MailMessage
                {
                    From = new MailAddress(_settings.FromEmail, _settings.DisplayName),
                    Subject = asunto,
                    Body = cuerpo,
                    IsBodyHtml = true
                };

                mensaje.To.Add(destino);

                using var smtp = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
                {
                    Credentials = new NetworkCredential(_settings.SmtpUser, _settings.SmtpPass),
                    EnableSsl = true
                };

                await smtp.SendMailAsync(mensaje);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error enviando correo: {ex.Message}");
                return false;
            }
        }
    }
}
