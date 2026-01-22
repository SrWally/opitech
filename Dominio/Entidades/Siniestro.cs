namespace Dominio.Entidades
{
    public class Siniestro
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int CiudadId { get; set; }
        public int TipoSiniestroId { get; set; }  
        public int VehiculosInvolucrados { get; set; }
        public int NumeroVictimas { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        
        public Ciudad? Ciudad { get; set; }
        public TipoSiniestro? TipoSiniestro { get; set; } 
    }
}