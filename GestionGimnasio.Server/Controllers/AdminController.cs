using GestionGimnasio.BD.Datos;
using Microsoft.AspNetCore.Mvc;

namespace GestionGimnasio.Server.Controllers
{
    [ApiController]
    [Route("admin/lista-usuarios")] //ruta base para las solicitudes HTTP relacionadas con usuarios
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

        [HttpGet] //admin/lista-usuarios
        public async Task<ActionResult<List<Usuario>>> GetUsuarios() 
        {
            var usuarios = await context.Usuarios.ToListAsync(); 
            if(usuarios == null || usuarios.Count == 0) 
            {
                return NotFound("No se encontraron usuarios"); 
            })
            return Ok(usuarios); //HTTP 200 OK
        }
    }
}
