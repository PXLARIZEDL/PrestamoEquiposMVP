using System;
using PrestamoEquipos.Repositorios;
using PrestamoEquipos.Servicios;
using PrestamoEquipos.Vistas;

namespace PrestamoEquipos.Presentadores
{
    /// <summary>
    /// Coordina la pantalla de prestamos. Delega las reglas al servicio y
    /// solo se encarga de refrescar la vista con lo que este devuelve.
    /// </summary>
    public class PrestamoPresenter
    {
        private readonly IPrestamoView _vista;
        private readonly IPrestamoService _servicio;
        private readonly IEquipoRepositorio _equipos;
        private readonly IUsuarioRepositorio _usuarios;

        public PrestamoPresenter(
            IPrestamoView vista,
            IPrestamoService servicio,
            IEquipoRepositorio equipos,
            IUsuarioRepositorio usuarios)
        {
            _vista = vista;
            _servicio = servicio;
            _equipos = equipos;
            _usuarios = usuarios;

            _vista.RegistrarPrestamo += OnRegistrarPrestamo;
            _vista.RegistrarDevolucion += OnRegistrarDevolucion;

            Refrescar();
        }

        private void OnRegistrarPrestamo(object sender, EventArgs e)
        {
            Resultado resultado = _servicio.RegistrarPrestamo(
                _vista.EquipoSeleccionado, _vista.UsuarioSeleccionado);

            _vista.MostrarMensaje(resultado.Mensaje);
            if (resultado.Exito)
                Refrescar();
        }

        private void OnRegistrarDevolucion(object sender, EventArgs e)
        {
            Resultado resultado = _servicio.RegistrarDevolucion(_vista.PrestamoSeleccionado);

            _vista.MostrarMensaje(resultado.Mensaje);
            if (resultado.Exito)
                Refrescar();
        }

        private void Refrescar()
        {
            _vista.CargarEquipos(_equipos.ObtenerTodos());
            _vista.CargarUsuarios(_usuarios.ObtenerTodos());
            _vista.MostrarActivos(_servicio.ObtenerPrestamosActivos());
            _vista.MostrarHistorial(_servicio.ObtenerHistorial());
        }
    }
}
