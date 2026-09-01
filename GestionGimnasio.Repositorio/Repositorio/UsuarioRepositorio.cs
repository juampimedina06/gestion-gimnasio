using GestionGimnasio.BD.Datos;
using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.BD.Datos.Entity.Enums;
using GestionGimnasio.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace GestionGimnasio.Repositorio.Repositorio
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> ExisteEmail(string email)
        {
            return await context.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task<Usuario?> SelectByEmail(string email)
        {
            return await context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> RegistrarPersonalTrainer(UsuarioDTO usuarioDTO)
        {
            if (await ExisteEmail(usuarioDTO.Email))
            {
                return null;
            }

            var rolPersonalTrainer = await context.Roles.FirstAsync(r => r.Tipo == TipoRol.PersonalTrainer);

            Usuario entidad = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                Apellido = usuarioDTO.Apellido,
                Email = usuarioDTO.Email,
                Contrasena = usuarioDTO.Contrasena,
                Activo = usuarioDTO.Activo,
                IdRol = rolPersonalTrainer.Id
            };

            return await Insert(entidad);
        }

        public async Task<Usuario?> Login(string email, string contrasena)
        {
            var usuario = await SelectByEmail(email);
            if (usuario == null || !usuario.Activo || usuario.Contrasena != contrasena)
            {
                return null;
            }

            return usuario;
        }
    }
}
