using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraClasesApp.Models
{
    public class MensajeEnviado
    {
        public int Id { get; set; }

        [Required]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public string Destinatario { get; set; } = string.Empty;

        [Required]
        public string Contenido { get; set; } = string.Empty;

        [Required]
        public DateTime FechaEnvio { get; set; }

        [ForeignKey("Tutor")]
        public int? TutorId { get; set; }
        public Tutor? Tutor { get; set; }

        [ForeignKey("ClaseExtra")]
        public int? ClaseExtraId { get; set; }
        public ClaseExtra? ClaseExtra { get; set; }
    }
}
