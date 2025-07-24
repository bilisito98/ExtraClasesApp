using System.ComponentModel.DataAnnotations;

namespace ExtraClasesApp.Models
{
    public class Tutor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Debe ser un número válido.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El país es obligatorio.")]
        public string Pais { get; set; } = string.Empty;

        public ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();

        public ICollection<ClaseExtra> ClasesExtra { get; set; } = new List<ClaseExtra>();
    }
}
