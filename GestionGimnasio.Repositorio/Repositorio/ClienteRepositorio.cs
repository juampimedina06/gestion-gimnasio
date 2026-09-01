using GestionGimnasio.BD.Datos;
using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.BD.Datos.Entity.Enums;
using GestionGimnasio.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Repositorio.Repositorio
{
    public class ClienteRepositorio : Repositorio<Cliente>, IClienteRepositorio
    {
        public ClienteRepositorio(AppDbContext context) : base(context)
        {
        }

        //lista de clientes con pagos, incluyendo la fecha del último pago y la fecha de vencimiento del plan
        public async Task<List<ListaClientesDTO>> SelectClientesConPago()
        {
            return await context.Clientes
                .Where(c => c.Pagos.Any())
                .Select(c => new ListaClientesDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Dni = c.Dni,
                    Email = c.Email,
                    Telefono = c.Telefono,
                    FechaPago = c.Pagos.Max(p => p.FechaPago),
                    FechaVencimiento = c.Pagos
                        .OrderByDescending(p => p.FechaPago)
                        .Select(p => p.FechaVencimiento)
                        .FirstOrDefault(),
                    Plan = new PlanDTO
                    {
                        Id = c.Pagos.OrderByDescending(p => p.FechaPago).First().Plan.Id,
                        Pase = c.Pagos.OrderByDescending(p => p.FechaPago).First().Plan.Pase,
                        Tipo = c.Pagos.OrderByDescending(p => p.FechaPago).First().Plan.Tipo,
                        DiasValidez = c.Pagos.OrderByDescending(p => p.FechaPago).First().Plan.DiasValidez,
                        DiasPorSemana = c.Pagos.OrderByDescending(p => p.FechaPago).First().Plan.DiasPorSemana,
                        Precio = c.Pagos.OrderByDescending(p => p.FechaPago).First().Plan.Precio,
                        ToleranciaDias = c.Pagos.OrderByDescending(p => p.FechaPago).First().Plan.ToleranciaDias
                    }
                })
                .ToListAsync();
        }

        //registra al cliente con su pago y plan, sin plan válido o con DNI/email repetido no se registra
        public async Task<int?> RegistrarClienteConPago(RegistrarClienteDTO registro)
        {
            bool existeCliente = await context.Clientes.AnyAsync(c => c.Dni == registro.Dni || c.Email == registro.Email);
            if (existeCliente)
            {
                return null;
            }

            var plan = await context.Planes.FirstOrDefaultAsync(p => p.Id == registro.IdPlan);
            if (plan == null)
            {
                return null;
            }

            using var transaccion = await context.Database.BeginTransactionAsync();

            var cliente = new Cliente
            {
                Nombre = registro.Nombre,
                Dni = registro.Dni,
                Telefono = registro.Telefono,
                Email = registro.Email,
                FechaAlta = DateTime.Today,
                Estado = EstadoCliente.Activo
            };

            context.Clientes.Add(cliente);
            await context.SaveChangesAsync();

            var pago = new Pago
            {
                IdCliente = cliente.Id,
                IdPlan = plan.Id,
                FechaPago = DateTime.Today,
                FechaVencimiento = DateTime.Today.AddDays((int)plan.DiasValidez),
                Monto = plan.Precio,
                MetodoPago = registro.MetodoPago,
                Estado = EstadoPago.AlDia
            };

            context.Pagos.Add(pago);
            await context.SaveChangesAsync();

            await transaccion.CommitAsync();

            return cliente.Id;
        }

        //registra el ingreso del cliente al gimnasio validando su DNI y el estado de su cuota
        public async Task<ResultadoIngresoDTO> RegistrarIngresoCliente(RegistroIngresoDTO ingreso)
        {
            var cliente = await context.Clientes
                .Include(c => c.Pagos)
                    .ThenInclude(p => p.Plan)
                .FirstOrDefaultAsync(c => c.Dni == ingreso.Dni);

            if (cliente == null)
            {
                return new ResultadoIngresoDTO { Permitido = false, Mensaje = "DNI incorrecto" };
            }

            var ultimoPago = cliente.Pagos
                .OrderByDescending(p => p.FechaPago)
                .FirstOrDefault();

            if (ultimoPago == null)
            {
                context.Ingresos.Add(new Ingreso
                {
                    IdCliente = cliente.Id,
                    FechaHora = DateTime.Now,
                    Permitido = false,
                    Observacion = "Cuota vencida"
                });
                await context.SaveChangesAsync();
                return new ResultadoIngresoDTO
                {
                    Permitido = false,
                    NombreCliente = cliente.Nombre,
                    Mensaje = "Cuota vencida"
                };
            }

            var plan = ultimoPago.Plan;
            int tolerancia = plan?.ToleranciaDias ?? 5;

            var resultado = new ResultadoIngresoDTO
            {
                NombreCliente = cliente.Nombre,
                FechaVencimiento = ultimoPago.FechaVencimiento,
                FechaLimitePago = ultimoPago.FechaVencimiento.AddDays(tolerancia)
            };

            // cuota al día: no venció
            if (DateTime.Today <= ultimoPago.FechaVencimiento)
            {
                resultado.Permitido = true;
                resultado.Mensaje = "Ingreso permitido. Cuota al día.";
            }
            // pasó más de la tolerancia desde el vencimiento: crítico
            else if (DateTime.Today > ultimoPago.FechaVencimiento.AddDays(tolerancia))
            {
                resultado.Permitido = false;
                resultado.Mensaje = $"Cuota vencida - estado crítico. Tenés hasta el {resultado.FechaLimitePago:dd/MM/yyyy} para pagar (tolerancia de {tolerancia} días desde el vencimiento).";
            }
            // venció pero está dentro de la tolerancia
            else
            {
                resultado.Permitido = true;
                resultado.Mensaje = $"Ingreso permitido, cuota vencida. Tenés hasta el {resultado.FechaLimitePago:dd/MM/yyyy} para pagar.";
            }

            context.Ingresos.Add(new Ingreso
            {
                IdCliente = cliente.Id,
                FechaHora = DateTime.Now,
                Permitido = resultado.Permitido,
                Observacion = resultado.Permitido ? "Ingreso validado" : resultado.Mensaje
            });
            await context.SaveChangesAsync();

            return resultado;
        }

        //renueva la cuota de un cliente creando un nuevo pago, solo si el último pago venció
        public async Task<ResultadoActualizarPagoDTO> ActualizarPago(ActualizarPagoDTO actualizar)
        {
            var cliente = await context.Clientes
                .Include(c => c.Pagos)
                    .ThenInclude(p => p.Plan)
                .FirstOrDefaultAsync(c => c.Dni == actualizar.Dni);

            if (cliente == null)
            {
                return new ResultadoActualizarPagoDTO { Exito = false, Mensaje = "Cliente no encontrado" };
            }

            var ultimoPago = cliente.Pagos
                .OrderByDescending(p => p.FechaPago)
                .FirstOrDefault();

            if (ultimoPago == null)
            {
                return new ResultadoActualizarPagoDTO
                {
                    Exito = false,
                    NombreCliente = cliente.Nombre,
                    Mensaje = "El cliente nunca realizó un pago, no se puede renovar"
                };
            }

            if (DateTime.Today <= ultimoPago.FechaVencimiento)
            {
                return new ResultadoActualizarPagoDTO
                {
                    Exito = false,
                    NombreCliente = cliente.Nombre,
                    Mensaje = "La cuota aún no venció"
                };
            }

            var plan = await context.Planes.FirstOrDefaultAsync(p => p.Id == actualizar.IdPlan);
            if (plan == null)
            {
                return new ResultadoActualizarPagoDTO
                {
                    Exito = false,
                    NombreCliente = cliente.Nombre,
                    Mensaje = "Plan no encontrado"
                };
            }

            using var transaccion = await context.Database.BeginTransactionAsync();

            var nuevoPago = new Pago
            {
                IdCliente = cliente.Id,
                IdPlan = plan.Id,
                FechaPago = DateTime.Today,
                FechaVencimiento = DateTime.Today.AddDays((int)plan.DiasValidez),
                Monto = plan.Precio,
                MetodoPago = actualizar.MetodoPago,
                Estado = EstadoPago.AlDia
            };

            context.Pagos.Add(nuevoPago);
            await context.SaveChangesAsync();

            await transaccion.CommitAsync();

            return new ResultadoActualizarPagoDTO
            {
                Exito = true,
                Mensaje = "Cuota renovada correctamente",
                NombreCliente = cliente.Nombre,
                FechaPago = nuevoPago.FechaPago,
                FechaVencimiento = nuevoPago.FechaVencimiento,
                Monto = nuevoPago.Monto
            };
        }
    }
}



