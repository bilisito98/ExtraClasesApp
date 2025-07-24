using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraClasesApp.Models
{
    public class Notificacion
    {
        public int Id { get; set; }

        // Relación con el Tutor
        [Required]
        public int TutorId { get; set; }

        [ForeignKey("TutorId")]
        public Tutor? Tutor { get; set; }

        // Asunto del correo
        [Required(ErrorMessage = "El asunto es obligatorio.")]
        [StringLength(100)]
        public string Asunto { get; set; } = string.Empty;

        // Correo electrónico del destinatario
        [Required(ErrorMessage = "El destinatario es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
        public string Destinatario { get; set; } = string.Empty;

        private DateTime _fechaEnvio;

        // Fecha y hora en que se envía la notificación (en UTC)
        [Required(ErrorMessage = "La fecha de envío es obligatoria.")]
        [Display(Name = "Fecha de Envío")]
        [DataType(DataType.DateTime)]
        public DateTime FechaEnvio
        {
            get => _fechaEnvio;
            set => _fechaEnvio = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        // Cuerpo del mensaje
        [Required(ErrorMessage = "El mensaje es obligatorio.")]
        public string Mensaje { get; set; } = string.Empty;
    }
}
