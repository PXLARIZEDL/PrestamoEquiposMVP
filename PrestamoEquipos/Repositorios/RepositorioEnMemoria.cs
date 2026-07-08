using System.Collections.Generic;
using System.Linq;
using PrestamoEquipos.Modelos;

namespace PrestamoEquipos.Repositorios
{
    /// <summary>
    /// Implementacion base en memoria. Reune la logica comun (lista + Id
    /// autoincremental) para no repetirla en cada repositorio (DRY).
    /// Si manana se quiere SQL, se crea otra clase que implemente la misma
    /// interfaz sin tocar el resto del sistema (OCP).
    /// </summary>
    public abstract class RepositorioEnMemoria<T> : IRepositorio<T> where T : IEntidad
    {
        protected readonly List<T> Items = new List<T>();
        private int _siguienteId = 1;

        public virtual void Agregar(T entidad)
        {
            entidad.Id = _siguienteId;
            _siguienteId++;
            Items.Add(entidad);
        }

        public IEnumerable<T> ObtenerTodos()
        {
            // Se devuelve una copia para que nadie modifique la lista interna.
            return Items.ToList();
        }

        public T ObtenerPorId(int id)
        {
            return Items.FirstOrDefault(x => x.Id == id);
        }
    }
}
