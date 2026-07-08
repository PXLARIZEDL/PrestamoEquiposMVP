using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PrestamoEquipos.Modelos;
using PrestamoEquipos.Presentadores;
using PrestamoEquipos.Repositorios;
using PrestamoEquipos.Servicios;
using PrestamoEquipos.Vistas;

namespace PrestamoEquipos.Formularios
{
    public partial class FrmPrestamos : Form, IPrestamoView
    {
        public FrmPrestamos(IPrestamoService servicio, IEquipoRepositorio equipos, IUsuarioRepositorio usuarios)
        {
            InitializeComponent();

            btnPrestar.Click += (s, e) => RegistrarPrestamo?.Invoke(this, EventArgs.Empty);
            btnDevolver.Click += (s, e) => RegistrarDevolucion?.Invoke(this, EventArgs.Empty);

            new PrestamoPresenter(this, servicio, equipos, usuarios);
        }

        public Equipo EquipoSeleccionado
        {
            get { return cboEquipo.SelectedItem as Equipo; }
        }

        public Usuario UsuarioSeleccionado
        {
            get { return cboUsuario.SelectedItem as Usuario; }
        }

        public Prestamo PrestamoSeleccionado
        {
            get
            {
                if (dgvActivos.CurrentRow == null)
                    return null;
                return dgvActivos.CurrentRow.Tag as Prestamo;
            }
        }

        public event EventHandler RegistrarPrestamo;
        public event EventHandler RegistrarDevolucion;

        public void CargarEquipos(IEnumerable<Equipo> equipos)
        {
            cboEquipo.DataSource = null;
            cboEquipo.DataSource = new List<Equipo>(equipos);
            cboEquipo.DisplayMember = "Nombre";
            cboEquipo.SelectedIndex = -1;
        }

        public void CargarUsuarios(IEnumerable<Usuario> usuarios)
        {
            cboUsuario.DataSource = null;
            cboUsuario.DataSource = new List<Usuario>(usuarios);
            cboUsuario.DisplayMember = "Nombre";
            cboUsuario.SelectedIndex = -1;
        }

        public void MostrarActivos(IEnumerable<Prestamo> activos)
        {
            dgvActivos.Rows.Clear();
            foreach (Prestamo p in activos)
            {
                int indice = dgvActivos.Rows.Add(
                    p.Equipo.Nombre,
                    p.Usuario.Nombre,
                    p.FechaPrestamo.ToString("dd/MM/yyyy HH:mm"));
                // Se guarda el objeto real en el Tag para recuperarlo al devolver.
                dgvActivos.Rows[indice].Tag = p;
            }
        }

        public void MostrarHistorial(IEnumerable<Prestamo> historial)
        {
            dgvHistorial.Rows.Clear();
            foreach (Prestamo p in historial)
            {
                string estado = p.EstaDevuelto ? "Devuelto" : "Activo";
                string fechaDev = p.EstaDevuelto
                    ? p.FechaDevolucion.Value.ToString("dd/MM/yyyy HH:mm")
                    : "-";

                dgvHistorial.Rows.Add(
                    p.Equipo.Nombre,
                    p.Usuario.Nombre,
                    p.FechaPrestamo.ToString("dd/MM/yyyy HH:mm"),
                    fechaDev,
                    estado);
            }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Prestamos", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
