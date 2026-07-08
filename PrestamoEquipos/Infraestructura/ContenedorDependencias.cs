using PrestamoEquipos.Modelos;
using PrestamoEquipos.Repositorios;
using PrestamoEquipos.Servicios;

namespace PrestamoEquipos.Infraestructura
{
    /// <summary>
    /// Composition Root: el UNICO lugar donde se crean las clases concretas
    /// y se conectan con sus interfaces. Todo lo demas depende de
    /// abstracciones. Aqui se cambiaria "EnMemoria" por "SQL" el dia de
    /// manana sin tocar presenters, servicios ni formularios (OCP + DIP).
    /// </summary>
    public class ContenedorDependencias
    {
        public IEquipoRepositorio Equipos { get; private set; }
        public IUsuarioRepositorio Usuarios { get; private set; }
        public IPrestamoRepositorio Prestamos { get; private set; }
        public IPrestamoService PrestamoService { get; private set; }

        public ContenedorDependencias()
        {
            Equipos = new EquipoRepositorioEnMemoria();
            Usuarios = new UsuarioRepositorioEnMemoria();
            Prestamos = new PrestamoRepositorioEnMemoria();
            PrestamoService = new PrestamoService(Prestamos);

            CargarDatosDePrueba();
        }

        private void CargarDatosDePrueba()
        {
            Equipos.Agregar(new Equipo { Nombre = "Laptop Dell Latitude", Codigo = "EQ-001", Disponible = true });
            Equipos.Agregar(new Equipo { Nombre = "Multimetro Fluke 117", Codigo = "EQ-002", Disponible = true });
            Equipos.Agregar(new Equipo { Nombre = "Proyector Epson", Codigo = "EQ-003", Disponible = true });

            Usuarios.Agregar(new Usuario { Nombre = "Ricardo Pimentel", Matricula = "2021-0001" });
            Usuarios.Agregar(new Usuario { Nombre = "Alondra Pimentel", Matricula = "2021-0002" });
            Usuarios.Agregar(new Usuario { Nombre = "Juan Perez", Matricula = "2021-0003" });
        }
    }
}
