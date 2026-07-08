namespace PrestamoEquipos.Modelos
{
    public class Usuario : IEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Matricula { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
