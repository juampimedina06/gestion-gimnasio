using System;
using System.Collections.Generic;
using System.Text;
using GestionGimnasio.BD.Datos.Entity.Enums;

namespace GestionGimnasio.Shared.DTO
{
    public class RolDTO
    {
        public int Id { get; set; }
        public TipoRol Tipo { get; set; } = new TipoRol();
    }
}
