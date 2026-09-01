using System.ComponentModel.DataAnnotations;

namespace GestionGimnasio.Shared.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El email es obligatorio")]
        [MaxLength(256)]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MaxLength(256)]
        public string Contrasena { get; set; } = string.Empty;
    }
}