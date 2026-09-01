namespace GestionGimnasio.Shared.DTO
{
    public class LoginRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int IdRol { get; set; }
        public RolDTO Rol { get; set; } = null!;
    }
}