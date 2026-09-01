using GestionGimnasio.BD.Datos;
using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.BD.Datos.Entity.Enums;
using GestionGimnasio.Repositorio.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Servicios
string connectionString = builder.Configuration.GetConnectionString("ConnSqlServer")
    ?? throw new InvalidOperationException("No existe la conexión con la base de datos.");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnSqlServer")));


builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<Repositorio<Usuario>, Repositorio<Usuario>>();
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

#endregion

var app = builder.Build();

#region Seeding
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Roles.Any())
    {
        db.Roles.Add(new Rol { Tipo = TipoRol.Admin });
        db.Roles.Add(new Rol { Tipo = TipoRol.PersonalTrainer });
        db.SaveChanges();
    }

    if (!db.Planes.Any())
    {
        db.Planes.AddRange(
            new Plan { Pase = PlanPase.Libre, Tipo = PlanTipo.Mensual, DiasValidez = PlanValidez.TreintaDias, DiasPorSemana = 7, Precio = 35000, ToleranciaDias = 5 },
            new Plan { Pase = PlanPase.TresDias, Tipo = PlanTipo.Mensual, DiasValidez = PlanValidez.TreintaDias, DiasPorSemana = 3, Precio = 25000, ToleranciaDias = 5 }
        );
        db.SaveChanges();
    }

    if (!db.Usuarios.Any(u => u.Email == "admin@gimnasio.com"))
    {
        var rolAdmin = db.Roles.First(r => r.Tipo == TipoRol.Admin);
        db.Usuarios.Add(new Usuario
        {
            Nombre = "Admin",
            Apellido = "Sistema",
            Email = "admin@gimnasio.com",
            Contrasena = "admin123",
            Activo = true,
            IdRol = rolAdmin.Id
        });
        db.SaveChanges();
    }

    if (!db.Clientes.Any())
    {
        var planMensual = db.Planes.First(p => p.Tipo == PlanTipo.Mensual && p.Pase == PlanPase.Libre);

        var clientePrueba = new Cliente
        {
            Nombre = "Carlos Pérez",
            Dni = 40123456,
            Telefono = "1155551234",
            Email = "carlos@test.com",
            FechaAlta = DateTime.Today.AddDays(-60),
            Estado = EstadoCliente.Activo
        };
        db.Clientes.Add(clientePrueba);
        db.SaveChanges();

        db.Pagos.Add(new Pago
        {
            IdCliente = clientePrueba.Id,
            IdPlan = planMensual.Id,
            FechaPago = DateTime.Today.AddDays(-45),
            FechaVencimiento = DateTime.Today.AddDays(-15),
            Monto = planMensual.Precio,
            MetodoPago = MetodoPago.Efectivo,
            Estado = EstadoPago.Vencido
        });
        db.SaveChanges();
    }
}
#endregion

#region MiddleWare
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
#endregion

app.Run();
