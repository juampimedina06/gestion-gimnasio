using System;
using System.ComponentModel.DataAnnotations;

namespace GestionGimnasio.Shared.DTO
{
    public class RegistroIngresoDTO
    {
        [Required(ErrorMessage = "El DNI es obligatorio")]
        [Range(1000000, 99999999, ErrorMessage = "El DNI no es válido")]
        public long Dni { get; set; }
    }

    public class ResultadoIngresoDTO
    {
        public bool Permitido { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string? NombreCliente { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaLimitePago { get; set; }
    }
}
