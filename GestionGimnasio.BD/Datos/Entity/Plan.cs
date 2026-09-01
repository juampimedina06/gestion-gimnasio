using GestionGimnasio.BD.Datos.Entity.Enums;

namespace GestionGimnasio.BD.Datos.Entity;

/// <summary>
/// Plan de suscripción que ofrece el gimnasio.
/// </summary>
public class Plan
{
    public int Id { get; set; }
    public PlanPase Pase { get; set; }
    public PlanTipo Tipo { get; set; }
    public PlanValidez DiasValidez { get; set; }
    public int DiasPorSemana { get; set; }
    public decimal Precio { get; set; }
    public int ToleranciaDias { get; set; } = 5;

    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
