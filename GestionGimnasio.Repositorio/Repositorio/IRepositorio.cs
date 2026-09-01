namespace GestionGimnasio.Repositorio.Repositorio
{
    public interface IRepositorio<E> where E : class
    {
        Task<List<E>> Select();
    }
}