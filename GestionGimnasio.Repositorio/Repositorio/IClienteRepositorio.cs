using GestionGimnasio.BD.Datos.Entity;
using GestionGimnasio.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Repositorio.Repositorio
{
    public interface IClienteRepositorio : IRepositorio<Cliente>
    {
        Task<List<ListaClientesDTO>> SelectClientesConPago();
        Task<int?> RegistrarClienteConPago(RegistrarClienteDTO registro);
        Task<ResultadoIngresoDTO> RegistrarIngresoCliente(RegistroIngresoDTO ingreso);
        Task<ResultadoActualizarPagoDTO> ActualizarPago(ActualizarPagoDTO actualizar);
    }
}
