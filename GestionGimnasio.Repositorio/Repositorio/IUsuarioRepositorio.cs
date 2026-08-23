using GestionGimnasio.BD.Datos.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Repositorio.Repositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task Select();
    }
}
