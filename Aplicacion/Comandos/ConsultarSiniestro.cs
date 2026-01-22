using MediatR;
using Aplicacion.DTO;

namespace Aplicacion.Comandos
{
    public class ConsultarSiniestros : IRequest<PaginatedResult<SiniestroResponseDTO>>
    {
        public int? DepartamentoId { get; }
        public DateTime? FechaDesde { get; }
        public DateTime? FechaHasta { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        
        public ConsultarSiniestros(
            int? departamentoId,
            DateTime? fechaDesde,
            DateTime? fechaHasta,
            int pageNumber,
            int pageSize)
        {
            DepartamentoId = departamentoId;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            PageNumber = pageNumber;
            PageSize = Math.Min(pageSize, 100);
        }
        
        public ConsultarSiniestros(SiniestroQueryParams queryParams)
            : this(queryParams.DepartamentoId,
                  queryParams.FechaInicio,
                  queryParams.FechaFin,
                  queryParams.Pagina,
                  queryParams.TamanoPagina)
        {
        }
    }
}