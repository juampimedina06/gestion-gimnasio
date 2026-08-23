using GestionGimnasio.BD.Datos.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionGimnasio.Shared.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Contrasena { get; set; } = string.Empty;

        public bool Activo { get; set; }

        public int IdRol { get; set; } 
        public Rol Rol { get; set; } = null!;
    }
}
