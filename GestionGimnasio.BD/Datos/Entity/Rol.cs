using GestionGimnasio.BD.Datos.Entity.Enums;

namespace GestionGimnasio.BD.Datos.Entity;

/// <summary>
/// Rol que puede asumir un usuario del sistema.
/// </summary>
public class Rol
{
    public int Id { get; set; }
    public TipoRol Tipo { get; set; }

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
