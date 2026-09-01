using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.BD.Datos.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionGimnasio.Shared.DTO
{
    public class PagoDTO
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public int IdPlan { get; set; }

        public DateTime FechaPago { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Monto { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public EstadoPago Estado { get; set; }
    }

    public class ActualizarPagoDTO
    {
        [Required(ErrorMessage = "El DNI es obligatorio")]
        [Range(1000000, 99999999, ErrorMessage = "El DNI no es válido")]
        public long Dni { get; set; }

        [Required(ErrorMessage = "El plan es obligatorio")]
        public int IdPlan { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        public MetodoPago MetodoPago { get; set; }
    }

    public class ResultadoActualizarPagoDTO
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string? NombreCliente { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? Monto { get; set; }
    }
}
