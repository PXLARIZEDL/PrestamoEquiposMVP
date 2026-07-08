using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PrestamoEquipos.Modelos;
using PrestamoEquipos.Presentadores;
using PrestamoEquipos.Repositorios;
using PrestamoEquipos.Vistas;

namespace PrestamoEquipos.Formularios
{
    public partial class FrmEquipos : Form, IEquipoView
    {
        public FrmEquipos(IEquipoRepositorio repositorio)
        {
            InitializeComponent();

            btnGuardar.Click += (s, e) => GuardarEquipo?.Invoke(this, EventArgs.Empty);

            // El presenter se suscribe a los eventos de esta vista; esa
            // suscripcion lo mantiene vivo mientras viva el formulario.
            new EquipoPresenter(this, repositorio);
        }

        public string NombreEquipo
        {
            get { return txtNombre.Text.Trim(); }
        }

        public string CodigoEquipo
        {
            get { return txtCodigo.Text.Trim(); }
        }

        public event EventHandler GuardarEquipo;

        public void MostrarEquipos(IEnumerable<Equipo> equipos)
        {
            dgvEquipos.DataSource = null;
            dgvEquipos.DataSource = equipos.ToList();
        }

        public void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtCodigo.Clear();
            txtNombre.Focus();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Equipos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
