namespace Dominio.Entidades
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        
        public ICollection<Ciudad> Ciudades { get; set; } = new List<Ciudad>();
    }
}