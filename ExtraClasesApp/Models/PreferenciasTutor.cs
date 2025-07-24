using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraClasesApp.Models
{
    public class PreferenciasTutor
    {
        public int Id { get; set; }

        [Required]
        public int TutorId { get; set; }

        [ForeignKey("TutorId")]
        public Tutor? Tutor { get; set; }

        [Display(Name = "Notificaciones por SMS")]
        public bool RecibirNotificacionesSMS { get; set; }

        [Display(Name = "Notificaciones por Email")]
        public bool RecibirNotificacionesEmail { get; set; }

        [Display(Name = "Hora Resumen Diario")]
        public TimeSpan HoraResumenDiario { get; set; }

        [Required]
        [Display(Name = "Zona Horaria")]
        public string ZonaHoraria { get; set; } = string.Empty;
    }
}
