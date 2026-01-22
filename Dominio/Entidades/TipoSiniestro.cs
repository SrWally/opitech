namespace Dominio.Entidades
{
    public class TipoSiniestro
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        
        public ICollection<Siniestro> Siniestros { get; set; } = new List<Siniestro>();
    }
}