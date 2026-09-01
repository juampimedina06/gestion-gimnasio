using GestionGimnasio.Repositorio.Repositorio;
using GestionGimnasio.Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GestionGimnasio.Server.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositorio clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            this.clienteRepositorio = clienteRepositorio;
        }

        //POST : /cliente/registro
        //el personal trainer registra al cliente con su pago y plan; sin pago/plan válido no se registra
        [HttpPost("registro")]
        public async Task<ActionResult<int>> RegistrarCliente(RegistrarClienteDTO registro)
        {
            var idCliente = await clienteRepositorio.RegistrarClienteConPago(registro);

            if (idCliente == null)
            {
                return BadRequest("No se pudo registrar el cliente. Verificá que el plan exista y que el DNI o email no estén ya registrados.");
            }

            return Ok(idCliente);
        }



        //GET : /cliente/lista-clientes
        //devuelve la lista de clientes con su pago y su plan, si no tiene pago no se devuelve el cliente
        [HttpGet("lista-clientes")]
        public async Task<ActionResult<List<ListaClientesDTO>>> ListaClientes()
        {
            var clientes = await clienteRepositorio.SelectClientesConPago();

            if(clientes == null || clientes.Count == 0)
            {
                return NotFound("No se encontraron clientes con pago vigente.");
            }

            return Ok(clientes);
        }


        //POST : /cliente/ingreso
        //para que el cliente pueda ingresar al gimnasio, se valida que tenga un pago vigente y que no esté vencido, tiene que ingresar dni
        [HttpPost("ingreso")]
        public async Task<ActionResult<ResultadoIngresoDTO>> IngresoCliente(RegistroIngresoDTO ingreso)
        {
            var resultado = await clienteRepositorio.RegistrarIngresoCliente(ingreso);

            if (resultado.Permitido)
            {
                return Ok(resultado);
            }

            return BadRequest(resultado);
        }


        //POST : /cliente/actualizar
        //actualiza el cliente y su pago, si no tiene pago no se puede actualizar el cliente
        [HttpPost("actualizar")]
        public async Task<ActionResult<ResultadoActualizarPagoDTO>> ActualizarPago(ActualizarPagoDTO actualizar)
        {
            var resultado = await clienteRepositorio.ActualizarPago(actualizar);

            if (resultado.Exito)
            {
                return Ok(resultado);
            }

            return BadRequest(resultado);
        }
    }
}
