using System;
using System.Windows.Forms;
using PrestamoEquipos.Infraestructura;

namespace PrestamoEquipos.Formularios
{
    public partial class FrmPrincipal : Form
    {
        private readonly ContenedorDependencias _contenedor;

        public FrmPrincipal(ContenedorDependencias contenedor)
        {
            InitializeComponent();
            _contenedor = contenedor;

            btnEquipos.Click += BtnEquipos_Click;
            btnUsuarios.Click += BtnUsuarios_Click;
            btnPrestamos.Click += BtnPrestamos_Click;
        }

        private void BtnEquipos_Click(object sender, EventArgs e)
        {
            using (FrmEquipos frm = new FrmEquipos(_contenedor.Equipos))
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            using (FrmUsuarios frm = new FrmUsuarios(_contenedor.Usuarios))
            {
                frm.ShowDialog(this);
            }
        }

        private void BtnPrestamos_Click(object sender, EventArgs e)
        {
            using (FrmPrestamos frm = new FrmPrestamos(
                _contenedor.PrestamoService, _contenedor.Equipos, _contenedor.Usuarios))
            {
                frm.ShowDialog(this);
            }
        }
    }
}
