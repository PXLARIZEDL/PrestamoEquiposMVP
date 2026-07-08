using System;
using System.Windows.Forms;
using PrestamoEquipos.Formularios;
using PrestamoEquipos.Infraestructura;

namespace PrestamoEquipos
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Se crea el contenedor una sola vez y se comparte con toda la app,
            // asi los datos viven durante toda la sesion.
            ContenedorDependencias contenedor = new ContenedorDependencias();
            Application.Run(new FrmPrincipal(contenedor));
        }
    }
}
