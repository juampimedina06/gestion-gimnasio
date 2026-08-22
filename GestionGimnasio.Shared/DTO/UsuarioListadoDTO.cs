using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Shared.DTO
{
    public class UsuarioListadoDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool Activo { get; set; }

        public int IdRol { get; set; }
        public RolDTO Rol { get; set; } = null!;
    }
}
