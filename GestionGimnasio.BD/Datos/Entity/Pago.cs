using GestionGimnasio.BD.Datos.Entity.Enums;

namespace GestionGimnasio.BD.Datos.Entity;

/// <summary>
/// Pago realizado por un cliente para acceder a un plan.
/// </summary>
public class Pago
{
    public int Id { get; set; }

    public int IdCliente { get; set; }
    public Cliente Cliente { get; set; } = null!;

    public int IdPlan { get; set; }
    public Plan Plan { get; set; } = null!;

    public DateTime FechaPago { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public decimal Monto { get; set; }
    public MetodoPago MetodoPago { get; set; }
    public EstadoPago Estado { get; set; }
}
