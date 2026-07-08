namespace PrestamoEquipos.Modelos
{
    /// <summary>
    /// Contrato minimo que cumplen todas las entidades del dominio.
    /// Permite que el repositorio generico asigne y busque por Id
    /// sin conocer el tipo concreto (soporta OCP y DIP).
    /// </summary>
    public interface IEntidad
    {
        int Id { get; set; }
    }
}
