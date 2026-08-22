using System.ComponentModel.DataAnnotations;

namespace GestionGimnasio.BD.Datos.Entity;

/// <summary>
/// Registro de ingreso de un cliente al gimnasio.
/// </summary>
public class Ingreso
{
    public int Id { get; set; }

    public int IdCliente { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public DateTime FechaHora { get; set; }
    public bool Permitido { get; set; }

    [MaxLength(500, ErrorMessage = "La observación no puede tener más de 500 caracteres")]
    public string? Observacion { get; set; }
}
