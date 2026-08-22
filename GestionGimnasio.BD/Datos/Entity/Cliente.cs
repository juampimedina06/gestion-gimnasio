using System.ComponentModel.DataAnnotations;
using GestionGimnasio.BD.Datos.Entity.Enums;

namespace GestionGimnasio.BD.Datos.Entity;

/// <summary>
/// Cliente socio del gimnasio, vinculado opcionalmente a una cuenta de usuario.
/// </summary>
public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    public long Dni { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [MaxLength(20, ErrorMessage = "El teléfono no puede tener más de 20 caracteres")]
    public string Telefono { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [MaxLength(256, ErrorMessage = "El email no puede tener más de 256 caracteres")]
    public string Email { get; set; } = string.Empty;

    public DateTime FechaAlta { get; set; }
    public EstadoCliente Estado { get; set; }

    public int? IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
    public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
}
