using System;
using PrestamoEquipos.Modelos;
using PrestamoEquipos.Repositorios;
using PrestamoEquipos.Vistas;

namespace PrestamoEquipos.Presentadores
{
    public class UsuarioPresenter
    {
        private readonly IUsuarioView _vista;
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioPresenter(IUsuarioView vista, IUsuarioRepositorio repositorio)
        {
            _vista = vista;
            _repositorio = repositorio;

            _vista.GuardarUsuario += OnGuardarUsuario;

            CargarUsuarios();
        }

        private void OnGuardarUsuario(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_vista.NombreUsuario))
            {
                _vista.MostrarMensaje("El nombre del usuario es obligatorio.");
                return;
            }

            Usuario usuario = new Usuario
            {
                Nombre = _vista.NombreUsuario,
                Matricula = _vista.MatriculaUsuario
            };

            _repositorio.Agregar(usuario);
            _vista.MostrarMensaje("Usuario registrado correctamente.");
            _vista.LimpiarFormulario();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            _vista.MostrarUsuarios(_repositorio.ObtenerTodos());
        }
    }
}
