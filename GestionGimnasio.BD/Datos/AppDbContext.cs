using GestionGimnasio.BD.Datos.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestionGimnasio.BD.Datos;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios => {get; set;};
    public DbSet<Rol> Roles => {get; set;};
    public DbSet<Plan> Planes => { get; set; };
    public DbSet<Cliente> Clientes => { get; set; };
    public DbSet<Pago> Pagos => { get; set; };
    public DbSet<Ingreso> Ingresos => { get; set; };
    public DbSet<Notificacion> Notificaciones => { get; set; };
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}
