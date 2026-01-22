using MediatR;
using Aplicacion.Comandos;
using Aplicacion.DTO;
using Dominio.Interfaces;

namespace Aplicacion.Handlers
{
    public class ObtenerSiniestroPorIdHandler : IRequestHandler<ObtenerSiniestroPorId, SiniestroResponseDTO?>
    {
        private readonly ITransaccionalidad _transaccionalidad;

        public ObtenerSiniestroPorIdHandler(ITransaccionalidad transaccionalidad)
        {
            _transaccionalidad = transaccionalidad;
        }

        public async Task<SiniestroResponseDTO?> Handle(ObtenerSiniestroPorId request, CancellationToken cancellationToken)
        {
            var siniestro = await _transaccionalidad.Siniestros.GetByIdAsync(request.Id);
            
            if (siniestro == null)
                return null;
            
            return new SiniestroResponseDTO
            {
                Id = siniestro.Id,
                FechaHora = siniestro.FechaHora,
                CiudadId = siniestro.CiudadId,
                Ciudad = siniestro.Ciudad?.Nombre ?? "Desconocido",
                Departamento = siniestro.Ciudad?.Departamento?.Nombre ?? "Desconocido",
                DepartamentoId = siniestro.Ciudad?.DepartamentoId ?? 0,
                TipoSiniestroId = siniestro.TipoSiniestroId,
                TipoSiniestro = siniestro.TipoSiniestro?.Nombre ?? "Desconocido",
                VehiculosInvolucrados = siniestro.VehiculosInvolucrados,
                NumeroVictimas = siniestro.NumeroVictimas,
                Descripcion = siniestro.Descripcion,
                FechaCreacion = siniestro.FechaCreacion
            };
        }
    }
}