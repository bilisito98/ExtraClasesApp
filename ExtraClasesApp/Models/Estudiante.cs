using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtraClasesApp.Models
{
    public class Estudiante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo del padre es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        [Display(Name = "Correo del padre o madre")]
        public string EmailPadre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Debe ingresar un número válido.")]
        [Display(Name = "Teléfono del padre o madre")]
        public string TelefonoPadre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El país es obligatorio.")]
        public string Pais { get; set; } = string.Empty;

        [Required(ErrorMessage = "El curso es obligatorio.")]
        public string Curso { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar un tutor.")]
        [Display(Name = "Tutor asignado")]
        public int TutorId { get; set; }

        public Tutor? Tutor { get; set; }

        public ICollection<ClaseExtra> ClasesExtra { get; set; } = new List<ClaseExtra>();
    }
}
