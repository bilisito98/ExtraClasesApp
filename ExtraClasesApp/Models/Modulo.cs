using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraClasesApp.Models
{
    public class Modulo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        // Relación con clases extra (opcional si usas esto en ClaseExtra)
        public ICollection<ClaseExtra>? ClasesExtra { get; set; }
    }
}
