using System.ComponentModel.DataAnnotations;
using GestionGimnasio.BD.Datos.Entity.Enums;

namespace GestionGimnasio.BD.Datos.Entity;

/// <summary>
/// Notificación enviada a un cliente por algún canal.
/// </summary>
public class Notificacion
{
    public int Id { get; set; }

    public int IdCliente { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public TipoNotificacion Tipo { get; set; }

    [Required(ErrorMessage = "El mensaje es obligatorio")]
    [MaxLength(500, ErrorMessage = "El mensaje no puede tener más de 500 caracteres")]
    public string Mensaje { get; set; } = string.Empty;
    public DateTime FechaEnvio { get; set; }
    public bool Enviado { get; set; }
}
