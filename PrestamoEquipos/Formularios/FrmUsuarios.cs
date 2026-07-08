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
    public partial class FrmUsuarios : Form, IUsuarioView
    {
        public FrmUsuarios(IUsuarioRepositorio repositorio)
        {
            InitializeComponent();

            btnGuardar.Click += (s, e) => GuardarUsuario?.Invoke(this, EventArgs.Empty);

            new UsuarioPresenter(this, repositorio);
        }

        public string NombreUsuario
        {
            get { return txtNombre.Text.Trim(); }
        }

        public string MatriculaUsuario
        {
            get { return txtMatricula.Text.Trim(); }
        }

        public event EventHandler GuardarUsuario;

        public void MostrarUsuarios(IEnumerable<Usuario> usuarios)
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarios.ToList();
        }

        public void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtMatricula.Clear();
            txtNombre.Focus();
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Usuarios", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
