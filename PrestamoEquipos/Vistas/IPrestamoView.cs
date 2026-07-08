using System;
using System.Collections.Generic;
using PrestamoEquipos.Modelos;

namespace PrestamoEquipos.Vistas
{
    public interface IPrestamoView
    {
        Equipo EquipoSeleccionado { get; }
        Usuario UsuarioSeleccionado { get; }
        Prestamo PrestamoSeleccionado { get; }

        event EventHandler RegistrarPrestamo;
        event EventHandler RegistrarDevolucion;

        void CargarEquipos(IEnumerable<Equipo> equipos);
        void CargarUsuarios(IEnumerable<Usuario> usuarios);
        void MostrarActivos(IEnumerable<Prestamo> activos);
        void MostrarHistorial(IEnumerable<Prestamo> historial);
        void MostrarMensaje(string mensaje);
    }
}
