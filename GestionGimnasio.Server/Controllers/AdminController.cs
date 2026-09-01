using GestionGimnasio.Repositorio.Repositorio;
using GestionGimnasio.Shared.DTO;
using Microsoft.AspNetCore.Mvc;


namespace GestionGimnasio.Server.Controllers
{
    [ApiController]
    [Route("admin")] //ruta base para las solicitudes HTTP relacionadas con usuarios
    public class AdminController : ControllerBase //clase que hereda de ControllerBase para manejar solicitudes HTTP relacionadas con usuarios
    {
        private readonly IUsuarioRepositorio usuarioRepositorio;

        public AdminController(IUsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        //POST : /admin/registro-usuario
        //dar de alta personal trainer. Solo PT, el admin se crea por seeding.
        [HttpPost("registro-usuario")]
        public async Task<ActionResult<int>> RegistrarUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = await usuarioRepositorio.RegistrarPersonalTrainer(usuarioDTO);
            if (usuario is null)
            {
                return BadRequest("Ya existe un usuario con ese email.");
            }

            return Ok(usuario.Id);
        }

        //POST : /admin/login
        //para q el admin y el personal trainer puedan loguearse
        [HttpPost("login")]
        public async Task<ActionResult<LoginRespuestaDTO>> Login(LoginDTO loginDTO)
        {
            var usuario = await usuarioRepositorio.Login(loginDTO.Email, loginDTO.Contrasena);
            if (usuario is null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            LoginRespuestaDTO respuesta = new LoginRespuestaDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                IdRol = usuario.IdRol,
                Rol = new RolDTO { Id = usuario.Rol.Id, Tipo = usuario.Rol.Tipo }
            };

            return Ok(respuesta);
        }
    }

}