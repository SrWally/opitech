using System.ComponentModel.DataAnnotations;

namespace Aplicacion.DTO
{
    public class RegistrarSiniestroDTO
    {
        [Required(ErrorMessage = "La fecha y hora son requeridas")]
        public DateTime FechaHora { get; set; }
        
        [Required(ErrorMessage = "La ciudad es requerida")]
        [Range(1, int.MaxValue, ErrorMessage = "La ciudad no es válida")]
        public int CiudadId { get; set; }
        
        [Required(ErrorMessage = "El tipo de siniestro es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El tipo de siniestro no es válido")]
        public int TipoSiniestroId { get; set; }
        
        [Required(ErrorMessage = "Los vehículos involucrados son requeridos")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe haber al menos 1 vehículo involucrado")]
        public int VehiculosInvolucrados { get; set; }
        
        [Required(ErrorMessage = "El número de víctimas es requerido")]
        [Range(0, int.MaxValue, ErrorMessage = "El número de víctimas no puede ser negativo")]
        public int NumeroVictimas { get; set; }
        
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]
        public string? Descripcion { get; set; }
    }
}