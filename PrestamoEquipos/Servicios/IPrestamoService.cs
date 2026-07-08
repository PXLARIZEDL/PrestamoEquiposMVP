using System.Collections.Generic;
using PrestamoEquipos.Modelos;

namespace PrestamoEquipos.Servicios
{
    public interface IPrestamoService
    {
        Resultado RegistrarPrestamo(Equipo equipo, Usuario usuario);
        Resultado RegistrarDevolucion(Prestamo prestamo);
        IEnumerable<Prestamo> ObtenerPrestamosActivos();
        IEnumerable<Prestamo> ObtenerHistorial();
    }
}
