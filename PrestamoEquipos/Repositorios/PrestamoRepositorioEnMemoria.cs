using System.Collections.Generic;
using System.Linq;
using PrestamoEquipos.Modelos;

namespace PrestamoEquipos.Repositorios
{
    public class PrestamoRepositorioEnMemoria : RepositorioEnMemoria<Prestamo>, IPrestamoRepositorio
    {
        public Prestamo ObtenerPrestamoActivoPorEquipo(int equipoId)
        {
            return Items.FirstOrDefault(p => p.Equipo.Id == equipoId && !p.EstaDevuelto);
        }

        public IEnumerable<Prestamo> ObtenerActivos()
        {
            return Items.Where(p => !p.EstaDevuelto).ToList();
        }
    }
}
