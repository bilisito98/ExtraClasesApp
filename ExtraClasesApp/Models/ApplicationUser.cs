using Microsoft.AspNetCore.Identity;

namespace ExtraClasesApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Puedes extender este modelo con más propiedades si lo deseas, por ejemplo:
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
