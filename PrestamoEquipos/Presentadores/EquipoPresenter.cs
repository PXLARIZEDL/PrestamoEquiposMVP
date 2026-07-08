using System;
using PrestamoEquipos.Modelos;
using PrestamoEquipos.Repositorios;
using PrestamoEquipos.Vistas;

namespace PrestamoEquipos.Presentadores
{
    /// <summary>
    /// Coordina la vista de equipos con el repositorio. Toda la logica de
    /// la pantalla vive aqui; el formulario solo dibuja (MVP + SRP).
    /// </summary>
    public class EquipoPresenter
    {
        private readonly IEquipoView _vista;
        private readonly IEquipoRepositorio _repositorio;

        public EquipoPresenter(IEquipoView vista, IEquipoRepositorio repositorio)
        {
            _vista = vista;
            _repositorio = repositorio;

            _vista.GuardarEquipo += OnGuardarEquipo;

            CargarEquipos();
        }

        private void OnGuardarEquipo(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_vista.NombreEquipo))
            {
                _vista.MostrarMensaje("El nombre del equipo es obligatorio.");
                return;
            }

            Equipo equipo = new Equipo
            {
                Nombre = _vista.NombreEquipo,
                Codigo = _vista.CodigoEquipo,
                Disponible = true
            };

            _repositorio.Agregar(equipo);
            _vista.MostrarMensaje("Equipo registrado correctamente.");
            _vista.LimpiarFormulario();
            CargarEquipos();
        }

        private void CargarEquipos()
        {
            _vista.MostrarEquipos(_repositorio.ObtenerTodos());
        }
    }
}
