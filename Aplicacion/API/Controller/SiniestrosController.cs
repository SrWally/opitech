using Microsoft.AspNetCore.Mvc;
using MediatR;
using Aplicacion.Comandos;
using Aplicacion.DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SiniestrosController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SiniestrosController> _logger;

        public SiniestrosController(IMediator mediator, ILogger<SiniestrosController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<SiniestroResponseDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ConsultarSiniestros(
            [FromQuery] SiniestroQueryParams queryParams)
        {
            try
            {
                // Validación de fechas
                if (queryParams.FechaInicio.HasValue && 
                    queryParams.FechaFin.HasValue && 
                    queryParams.FechaInicio > queryParams.FechaFin)
                {
                    return BadRequest(new { 
                        Error = "Rango de fechas inválido",
                        Detalle = "La fecha de inicio no puede ser mayor a la fecha fin" 
                    });
                }
                
                // Usar el nuevo constructor con queryParams
                var consulta = new ConsultarSiniestros(queryParams);
                var resultado = await _mediator.Send(consulta);
                
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    Mensaje = "Error al obtener siniestros", 
                    Error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Registra un nuevo siniestro vial
        /// </summary>
        /// <param name="dto">Datos del siniestro a registrar</param>
        /// <returns>ID del siniestro registrado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistrarSiniestro([FromBody] RegistrarSiniestroDTO dto)
        {
            try
            {
            
                var comando = new RegistrarSiniestro(
                    dto.FechaHora,
                    dto.CiudadId,
                    dto.TipoSiniestroId,
                    dto.VehiculosInvolucrados,
                    dto.NumeroVictimas,
                    dto.Descripcion);
                
                var siniestroId = await _mediator.Send(comando);
                
                
                return CreatedAtAction(
                    nameof(ConsultarSiniestros), 
                    new { id = siniestroId }, 
                    new { id = siniestroId, mensaje = "Siniestro registrado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Consulta siniestros viales con filtros opcionales
        /// </summary>
        /// <param name="departamentoId">ID del departamento para filtrar (opcional)</param>
        /// <param name="fechaDesde">Fecha desde para filtrar (opcional)</param>
        /// <param name="fechaHasta">Fecha hasta para filtrar (opcional)</param>
        /// <param name="pageNumber">Número de página (default: 1)</param>
        /// <param name="pageSize">Tamaño de página (default: 10, max: 100)</param>
        /// <returns>Lista paginada de siniestros</returns>
        /*HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<SiniestroResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConsultarSiniestros(
            [FromQuery] int? departamentoId = null,
            [FromQuery] DateTime? fechaDesde = null,
            [FromQuery] DateTime? fechaHasta = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                
                var consulta = new ConsultarSiniestros(
                    departamentoId,
                    fechaDesde,
                    fechaHasta,
                    pageNumber,
                    pageSize);
                
                var resultado = await _mediator.Send(consulta);
                
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }*/

        /// <summary>
        /// Obtiene un siniestro específico por su ID
        /// </summary>
        /// <param name="id">ID del siniestro</param>
        /// <returns>Datos del siniestro</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SiniestroResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerSiniestroPorId(int id)
{
    try
    {
        
        // Validar ID
        if (id <= 0)
        {
            return BadRequest(new { error = "El ID debe ser mayor a 0" });
        }
        
        // Crear comando para obtener el siniestro
        // Necesitamos crear este comando primero
        var consulta = new ObtenerSiniestroPorId(id);
        
        // Enviar al handler a través de MediatR
        var siniestro = await _mediator.Send(consulta);
        
        if (siniestro == null)
        {
            return NotFound(new { 
                error = $"No se encontró un siniestro con ID {id}",
                id = id 
            });
        }
        return Ok(siniestro);
    }
    catch (Exception ex)
    {
        return BadRequest(new { error = ex.Message });
    }
}
    }
}