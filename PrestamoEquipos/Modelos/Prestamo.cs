using System;

namespace PrestamoEquipos.Modelos
{
    public class Prestamo : IEntidad
    {
        public int Id { get; set; }
        public Equipo Equipo { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }

        /// <summary>Un prestamo esta devuelto cuando tiene fecha de devolucion.</summary>
        public bool EstaDevuelto
        {
            get { return FechaDevolucion.HasValue; }
        }
    }
}
