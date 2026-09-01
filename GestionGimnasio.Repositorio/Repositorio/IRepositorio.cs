using GestionGimnasio.BD.Datos.Entity.Interfaces;

namespace GestionGimnasio.Repositorio.Repositorio
{
    public interface IRepositorio<E> where E : class, IId
    {
        Task<List<E>> Select();
        Task<bool> Delete(int id);
        Task<bool> Existe(int id);
        Task<E> Insert(E entity);
        Task<E?> SelectById(int id);
        Task<bool> Update(E entity);
    }
}