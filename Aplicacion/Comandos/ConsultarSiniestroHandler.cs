
using MediatR;
using Dominio.Interfaces;
using Aplicacion.DTO;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Comandos
{
    public class ConsultarSiniestrosHandler : 
        IRequestHandler<ConsultarSiniestros, PaginatedResult<SiniestroResponseDTO>>
    {
        private readonly ITransaccionalidad _transaccionalidad;
        
        public ConsultarSiniestrosHandler(ITransaccionalidad transaccionalidad)
        {
            _transaccionalidad = transaccionalidad;
        }
        
        public async Task<PaginatedResult<SiniestroResponseDTO>> Handle(
            ConsultarSiniestros request, 
            CancellationToken cancellationToken)
        {
            var (siniestros, totalCount) = await _transaccionalidad.Siniestros.GetPagedAsync(
                request.DepartamentoId,
                request.FechaDesde,
                request.FechaHasta,
                request.PageNumber,
                request.PageSize);
            
            var items = siniestros.Select(MapToDto).ToList();
            
            return new PaginatedResult<SiniestroResponseDTO>(
                items, request.PageNumber, request.PageSize, totalCount);
        }
        
        private SiniestroResponseDTO MapToDto(Dominio.Entidades.Siniestro siniestro)
        {
            return new SiniestroResponseDTO
            {
                Id = siniestro.Id,
                FechaHora = siniestro.FechaHora,
                CiudadId = siniestro.CiudadId,
                Ciudad = siniestro.Ciudad?.Nombre ?? "Desconocido",
                DepartamentoId = siniestro.Ciudad?.DepartamentoId ?? 0,
                Departamento = siniestro.Ciudad?.Departamento?.Nombre ?? "Desconocido",
                TipoSiniestroId = siniestro.TipoSiniestroId,
                TipoSiniestro = siniestro.TipoSiniestro?.Descripcion ?? "Desconocido",
                VehiculosInvolucrados = siniestro.VehiculosInvolucrados,
                NumeroVictimas = siniestro.NumeroVictimas,
                Descripcion = siniestro.Descripcion,
                FechaCreacion = siniestro.FechaCreacion
            };
        }
    }
}