namespace PrestamoEquipos.Modelos
{
    public class Equipo : IEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public bool Disponible { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
