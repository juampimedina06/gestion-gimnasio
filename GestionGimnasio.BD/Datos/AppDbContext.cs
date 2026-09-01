using GestionGimnasio.BD.Datos.Entity;
using Microsoft.EntityFrameworkCore;

namespace GestionGimnasio.BD.Datos;

public class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; } = null!;
    public DbSet<Rol> Roles { get; set; } = null!;
    public DbSet<Plan> Planes { get; set; } = null!;
    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Pago> Pagos { get; set; } = null!;
    public DbSet<Ingreso> Ingresos { get; set; } = null!;
    public DbSet<Notificacion> Notificaciones { get; set; } = null!;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar precisión y escala para propiedades decimal
        modelBuilder.Entity<Pago>()
            .Property(p => p.Monto)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Plan>()
            .Property(p => p.Precio)
            .HasPrecision(18, 2);
    }
}
