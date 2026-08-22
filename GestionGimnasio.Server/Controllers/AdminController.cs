using GestionGimnasio.BD.Datos;
using GestionGimnasio.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GestionGimnasio.Server.Controllers
{
    [ApiController]
    [Route("admin")] //ruta base para las solicitudes HTTP relacionadas con usuarios
    public class AdminController : ControllerBase //clase que hereda de ControllerBase para manejar solicitudes HTTP relacionadas con usuarios
    {
        private readonly AppDbContext context; //variable privada para almacenar el contexto de la base de datos
        public AdminController(AppDbContext context) 
        {
            this.context = context; //inicializa el contexto de la base de datos
        }

        //[HttpGet]
        //public string Get()
        //{
        //    return "Hola desde UsuarioController"; 
        //}

        [HttpGet] //admin/usuario
        public async Task<ActionResult<List<Usuario>>> GetUsuario() 
        {
            var usuarios = await context.Usuarios.ToListAsync(); 
            if(usuarios == null || usuarios.Count == 0) 
            {
                return NotFound("No se encontraron usuarios"); 
            })
            return Ok(usuarios); //HTTP 200 OK
        }


        [HttpGet("lista-usuarios")] //admin/lista-usuarios
        public async Task<ActionResult<List<UsuarioListadoDTO>>> GetListaUsuario()
        {
            var lista = await context.Usuarios
                .Select(p => new UsuarioListadoDTO
                {
                    Id = p.Id,
                    Email = p.Email,
                    Activo = p.Activo,
                    IdRol = p.IdRol,
                    Rol = new RolDTO
                    {
                        Id = p.Rol.Id,
                        Nombre = p.Rol.Nombre
                    }
                })
                .ToListAsync();

            if(lista == null || lista.Count == 0)
            {
                return NotFound("No se encontraron usuarios");
            }
            return Ok(lista); //HTTP 200 OK>
        }
}
