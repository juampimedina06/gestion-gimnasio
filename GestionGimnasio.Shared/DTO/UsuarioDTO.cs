using System.ComponentModel.DataAnnotations;

namespace GestionGimnasio.Shared.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [MaxLength(256)]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MaxLength(256)]
        public string Contrasena { get; set; } = string.Empty;

        public bool Activo { get; set; }
    }
}
