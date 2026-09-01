using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.Shared.DTO;

namespace GestionGimnasio.Repositorio.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<bool> ExisteEmail(string email);
        Task<Usuario?> SelectByEmail(string email);
        Task<Usuario?> RegistrarPersonalTrainer(UsuarioDTO usuarioDTO);
        Task<Usuario?> Login(string email, string contrasena);
    }
}
