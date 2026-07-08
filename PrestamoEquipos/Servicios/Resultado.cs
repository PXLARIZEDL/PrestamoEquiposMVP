namespace PrestamoEquipos.Servicios
{
    /// <summary>
    /// Resultado de una operacion de negocio. Evita usar excepciones para
    /// controlar el flujo y le da al presenter un mensaje claro que mostrar.
    /// </summary>
    public class Resultado
    {
        public bool Exito { get; private set; }
        public string Mensaje { get; private set; }

        private Resultado(bool exito, string mensaje)
        {
            Exito = exito;
            Mensaje = mensaje;
        }

        public static Resultado Ok(string mensaje)
        {
            return new Resultado(true, mensaje);
        }

        public static Resultado Fallo(string mensaje)
        {
            return new Resultado(false, mensaje);
        }
    }
}
