using System.ComponentModel.DataAnnotations;
using GestionGimnasio.BD.Datos.Entity.Interfaces;

namespace GestionGimnasio.BD.Datos.Entity;

/// <summary>
/// Usuario con acceso al sistema (admin o personal trainer).
/// </summary>
public class Usuario : IId
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [MaxLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres")]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [MaxLength(256, ErrorMessage = "El email no puede tener más de 256 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [MaxLength(256, ErrorMessage = "La contraseña no puede tener más de 256 caracteres")]
    public string Contrasena { get; set; } = string.Empty;

    public bool Activo { get; set; }

    public int IdRol { get; set; }
    public Rol Rol { get; set; } = null!;
}
