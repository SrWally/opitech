using MediatR;

namespace Aplicacion.Comandos
{
    public class RegistrarSiniestro : IRequest<int>
    {
        public DateTime FechaHora { get; set; }
        public int CiudadId { get; set; }
        public int TipoSiniestroId { get; set; }
        public int VehiculosInvolucrados { get; set; }
        public int NumeroVictimas { get; set; }
        public string? Descripcion { get; set; }
        
        public RegistrarSiniestro(
            DateTime fechaHora,
            int ciudadId,
            int tipoSiniestroId,
            int vehiculosInvolucrados,
            int numeroVictimas,
            string? descripcion = null)
        {
            FechaHora = fechaHora;
            CiudadId = ciudadId;
            TipoSiniestroId = tipoSiniestroId;
            VehiculosInvolucrados = vehiculosInvolucrados;
            NumeroVictimas = numeroVictimas;
            Descripcion = descripcion;
        }
    }
}