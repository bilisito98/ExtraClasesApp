using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraClasesApp.Models
{
    public class ClaseExtra
    {
        public int Id { get; set; }

        private DateTime _fechaHoraTutor;
        private DateTime _fechaHoraEstudiante;

        [Required]
        [Display(Name = "Fecha y Hora del Tutor")]
        [DataType(DataType.DateTime)]
        public DateTime FechaHoraTutor
        {
            get => _fechaHoraTutor;
            set => _fechaHoraTutor = value.Kind == DateTimeKind.Utc
                ? value
                : DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        [Required]
        [Display(Name = "Fecha y Hora del Estudiante")]
        [DataType(DataType.DateTime)]
        public DateTime FechaHoraEstudiante
        {
            get => _fechaHoraEstudiante;
            set => _fechaHoraEstudiante = value.Kind == DateTimeKind.Utc
                ? value
                : DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        [Required]
        [StringLength(100)]
        public string Modulo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Leccion { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Estudiante")]
        [Display(Name = "Estudiante")]
        public int EstudianteId { get; set; }

        public Estudiante? Estudiante { get; set; }

        [Required]
        [ForeignKey("Tutor")]
        [Display(Name = "Tutor")]
        public int TutorId { get; set; }

        public Tutor? Tutor { get; set; }

        [Display(Name = "Notificación Enviada")]
        public bool Notificado { get; set; } = false;
    }
}
