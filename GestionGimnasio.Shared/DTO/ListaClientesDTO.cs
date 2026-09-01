using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Shared.DTO
{
    public class ListaClientesDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public long Dni { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public DateTime FechaPago { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public PlanDTO Plan { get; set; } = new();
    }
}
