using System.Collections.Generic;
using PrestamoEquipos.Modelos;

namespace PrestamoEquipos.Repositorios
{
    /// <summary>
    /// El repositorio de prestamos agrega solo lo que necesita su cliente:
    /// consultar activos y saber si un equipo ya esta prestado (ISP).
    /// </summary>
    public interface IPrestamoRepositorio : IRepositorio<Prestamo>
    {
        Prestamo ObtenerPrestamoActivoPorEquipo(int equipoId);
        IEnumerable<Prestamo> ObtenerActivos();
    }
}
