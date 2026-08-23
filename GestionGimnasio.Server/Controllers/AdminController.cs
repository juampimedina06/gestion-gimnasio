using GestionGimnasio.BD.Datos;
using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.Repositorio.Repositorio;
using GestionGimnasio.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionGimnasio.Server.Controllers
{
    [ApiController]
    [Route("admin")] //ruta base para las solicitudes HTTP relacionadas con usuarios
    public class AdminController : ControllerBase //clase que hereda de ControllerBase para manejar solicitudes HTTP relacionadas con usuarios
    {
        private readonly AppDbContext context; //variable privada para almacenar el contexto de la base de datos
        private readonly IUsuarioRepositorio repositorio;

        public AdminController(AppDbContext context,
            IUsuarioRepositorio repositorio)
        {
            this.context = context; //inicializa el contexto de la base de datos
            this.repositorio = repositorio; //inicializa el repositorio de usuarios
        }

        //[HttpGet]
        //public string Get()
        //{
        //    return "Hola desde UsuarioController"; 
        //}

        [HttpGet] //admin/usuario
        public async Task<ActionResult<List<Usuario>>> GetUsuario()
        {
            //var usuarios = await context.Usuarios.ToListAsync();
            //if (usuarios == null || usuarios.Count == 0)
            //{
            //    return NotFound("No se encontraron usuarios");
            //}
            //return Ok(usuarios); //HTTP 200 OK

            var lista = await repositorio.Select();
            if (lista == null) 
            {
                return NotFound("No se encontre elementos de la lista");
            }
            return Ok(lista);
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
                        Tipo = p.Rol.Tipo
                    }
                })
                .ToListAsync();

            if (lista == null || lista.Count == 0)
            {
                return NotFound("No se encontraron usuarios");
            }
            return Ok(lista); //HTTP 200 OK>
        }


        //ver un usuario en detalle
        [HttpGet("{id : int}")] //admin/usuario/{id}
        public async Task<ActionResult<UsuarioDTO>> GetById(int id)
        {
            var entidad = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
           if(entidad == null)
            {
                return NotFound($"No existe el usuario con el id: {id}");
            }
            UsuarioDTO DTO = new UsuarioDTO
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                Apellido = entidad.Apellido,
                Email = entidad.Email,
                Contrasena = entidad.Contrasena,
                Activo = entidad.Activo,
                IdRol = entidad.IdRol,
                Rol = new Rol
                {
                    Id = entidad.Rol.Id,
                    Tipo = entidad.Rol.Tipo
                }
            };

            return Ok(DTO);
        }






    }
      
}
