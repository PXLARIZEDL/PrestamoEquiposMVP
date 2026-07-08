using System.Collections.Generic;

namespace PrestamoEquipos.Repositorios
{
    /// <summary>
    /// Abstraccion generica de almacenamiento. Los presenters y servicios
    /// dependen de esta interfaz, no de la implementacion concreta (DIP).
    /// </summary>
    public interface IRepositorio<T>
    {
        void Agregar(T entidad);
        IEnumerable<T> ObtenerTodos();
        T ObtenerPorId(int id);
    }
}
