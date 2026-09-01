using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.BD.Datos.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Shared.DTO
{
    public class PlanDTO
    {
        public int Id { get; set; }
        public PlanPase Pase { get; set; }
        public PlanTipo Tipo { get; set; }
        public PlanValidez DiasValidez { get; set; }
        public int DiasPorSemana { get; set; }
        public decimal Precio { get; set; }
        public int ToleranciaDias { get; set; } = 5;
    }
}
