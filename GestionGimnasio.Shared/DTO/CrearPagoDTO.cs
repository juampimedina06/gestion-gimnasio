using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.BD.Datos.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Shared.DTO
{
    public class CrearPagoDTO
    {
        public int IdCliente { get; set; }
        public int IdPlan { get; set; }
        public decimal Monto { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}
