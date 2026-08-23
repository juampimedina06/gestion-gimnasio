using GestionGimnasio.BD.Datos;
using GestionGimnasio.BD.Datos.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionGimnasio.Repositorio.Repositorio
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        //sirve para heredar de la clase Repositorio y poder usar sus metodos
        public UsuarioRepositorio(AppDbContext context) : base(context)
        {
        }

        
    }
}
