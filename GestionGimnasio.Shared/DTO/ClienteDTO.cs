using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.BD.Datos.Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestionGimnasio.Shared.DTO
{
    public class CrearClienteDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public long Dni { get; set; }

        public string Telefono { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public DateTime FechaAlta { get; set; }
        public EstadoCliente Estado { get; set; }
        public int? IdUsuario { get; set; }

    }

    public class RegistrarClienteDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Range(1000000, 99999999, ErrorMessage = "El DNI no es válido")]
        public long Dni { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [MaxLength(20, ErrorMessage = "El teléfono no puede tener más de 20 caracteres")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [MaxLength(256, ErrorMessage = "El email no puede tener más de 256 caracteres")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "El plan es obligatorio")]
        public int IdPlan { get; set; }

        public MetodoPago MetodoPago { get; set; }
    }
}
