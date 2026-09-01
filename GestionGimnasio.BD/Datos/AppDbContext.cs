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

        // Configurar explícitamente las relaciones para evitar shadow properties.
        // Con la FK como "IdX" y la navegación como "X", EF esperaría "XId" por
        // convención y crearía columnas adicionales (RolId, UsuarioId, ClienteId, PlanId).
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Rol)
            .WithMany(r => r.Usuarios)
            .HasForeignKey(u => u.IdRol);

        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Usuario)
            .WithMany()
            .HasForeignKey(c => c.IdUsuario);

        modelBuilder.Entity<Pago>()
            .HasOne(p => p.Cliente)
            .WithMany(c => c.Pagos)
            .HasForeignKey(p => p.IdCliente);

        modelBuilder.Entity<Pago>()
            .HasOne(p => p.Plan)
            .WithMany(pl => pl.Pagos)
            .HasForeignKey(p => p.IdPlan);

        modelBuilder.Entity<Ingreso>()
            .HasOne(i => i.Cliente)
            .WithMany(c => c.Ingresos)
            .HasForeignKey(i => i.IdCliente);

        modelBuilder.Entity<Notificacion>()
            .HasOne(n => n.Cliente)
            .WithMany(c => c.Notificaciones)
            .HasForeignKey(n => n.IdCliente);
    }
}
