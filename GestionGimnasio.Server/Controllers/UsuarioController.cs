using Microsoft.AspNetCore.Mvc;

namespace GestionGimnasio.Server.Controllers
{
    [ApiController]
    [Route("api/usuario")] //ruta base para las solicitudes HTTP relacionadas con usuarios
    public class UsuarioController : ControllerBase //clase que hereda de ControllerBase para manejar solicitudes HTTP relacionadas con usuarios
    {
        [HttpGet]
        public string Get()
        {
            return "Hola desde UsuarioController"; 
        }
    }
}
