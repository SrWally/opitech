namespace Aplicacion.DTO
{
    public class SiniestroResponseDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public int CiudadId { get; set; }
        public string? Departamento { get; set; }
        public string? Ciudad { get; set; }
        public int DepartamentoId { get; set; }
        public int TipoSiniestroId { get; set; }
        public string? TipoSiniestro { get; set; } 
        public int VehiculosInvolucrados { get; set; }
        public int NumeroVictimas { get; set; }
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}