using System;
using System.Collections.Generic;
using PrestamoEquipos.Modelos;

namespace PrestamoEquipos.Vistas
{
    public interface IUsuarioView
    {
        string NombreUsuario { get; }
        string MatriculaUsuario { get; }

        event EventHandler GuardarUsuario;

        void MostrarUsuarios(IEnumerable<Usuario> usuarios);
        void LimpiarFormulario();
        void MostrarMensaje(string mensaje);
    }
}
