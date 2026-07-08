using System;
using System.Collections.Generic;
using PrestamoEquipos.Modelos;
using PrestamoEquipos.Repositorios;

namespace PrestamoEquipos.Servicios
{
    /// <summary>
    /// Contiene TODAS las reglas de negocio de prestamos. Unica
    /// responsabilidad (SRP). Depende de abstracciones de repositorio (DIP).
    /// </summary>
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepositorio _prestamos;

        public PrestamoService(IPrestamoRepositorio prestamos)
        {
            _prestamos = prestamos;
        }

        public Resultado RegistrarPrestamo(Equipo equipo, Usuario usuario)
        {
            if (equipo == null)
                return Resultado.Fallo("Debe seleccionar un equipo.");
            if (usuario == null)
                return Resultado.Fallo("Debe seleccionar un usuario.");

            // REGLA DE NEGOCIO: un equipo no puede estar prestado a mas de
            // una persona al mismo tiempo.
            Prestamo activo = _prestamos.ObtenerPrestamoActivoPorEquipo(equipo.Id);
            if (activo != null)
                return Resultado.Fallo("El equipo '" + equipo.Nombre +
                    "' ya esta prestado a " + activo.Usuario.Nombre + ".");

            Prestamo prestamo = new Prestamo
            {
                Equipo = equipo,
                Usuario = usuario,
                FechaPrestamo = DateTime.Now,
                FechaDevolucion = null
            };
            _prestamos.Agregar(prestamo);

            equipo.Disponible = false;

            return Resultado.Ok("Prestamo registrado: " + equipo.Nombre +
                " -> " + usuario.Nombre + ".");
        }

        public Resultado RegistrarDevolucion(Prestamo prestamo)
        {
            if (prestamo == null)
                return Resultado.Fallo("Debe seleccionar un prestamo activo.");
            if (prestamo.EstaDevuelto)
                return Resultado.Fallo("Ese prestamo ya fue devuelto.");

            prestamo.FechaDevolucion = DateTime.Now;
            prestamo.Equipo.Disponible = true;

            return Resultado.Ok("Devolucion registrada para el equipo '" +
                prestamo.Equipo.Nombre + "'.");
        }

        public IEnumerable<Prestamo> ObtenerPrestamosActivos()
        {
            return _prestamos.ObtenerActivos();
        }

        public IEnumerable<Prestamo> ObtenerHistorial()
        {
            return _prestamos.ObtenerTodos();
        }
    }
}
