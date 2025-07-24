// Services/EmailSettings.cs
namespace ExtraClasesApp.Services
{
    /// <summary>
    /// Configuración para el envío de correos electrónicos mediante SMTP.
    /// </summary>
    public class EmailSettings
    {
        /// <summary>
        /// Dirección de correo electrónico del remitente.
        /// </summary>
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        /// Nombre que se mostrará como remitente del correo.
        /// </summary>
        public string DisplayName { get; set; } = "ExtraClasesApp";

        /// <summary>
        /// Servidor SMTP, por ejemplo: smtp.gmail.com
        /// </summary>
        public string SmtpHost { get; set; } = string.Empty;

        /// <summary>
        /// Puerto SMTP (587 para TLS o 465 para SSL).
        /// </summary>
        public int SmtpPort { get; set; } = 587;

        /// <summary>
        /// Usuario de autenticación SMTP (normalmente es el mismo que FromEmail).
        /// </summary>
        public string SmtpUser { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña o clave generada por la aplicación para el SMTP.
        /// </summary>
        public string SmtpPass { get; set; } = string.Empty;
    }
}
