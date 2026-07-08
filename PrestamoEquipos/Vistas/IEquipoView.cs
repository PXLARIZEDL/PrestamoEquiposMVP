using System;
using System.Collections.Generic;
using PrestamoEquipos.Modelos;

namespace PrestamoEquipos.Vistas
{
    /// <summary>
    /// Contrato de la vista de equipos. El presenter conoce solo esta
    /// interfaz, nunca el formulario concreto (nucleo del patron MVP).
    /// </summary>
    public interface IEquipoView
    {
        string NombreEquipo { get; }
        string CodigoEquipo { get; }

        event EventHandler GuardarEquipo;

        void MostrarEquipos(IEnumerable<Equipo> equipos);
        void LimpiarFormulario();
        void MostrarMensaje(string mensaje);
    }
}
