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

        [HttpGet("Buscar")]
        [ProducesResponseType(typeof(PaginatedResult<SiniestroResponseDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ConsultarSiniestros(
            [FromQuery] SiniestroQueryParams queryParams)
        {
            try
            {
                if (queryParams.FechaInicio.HasValue && 
                    queryParams.FechaFin.HasValue && 
                    queryParams.FechaInicio > queryParams.FechaFin)
                {
                    return BadRequest(new { 
                        Error = "Rango de fechas inválido",
                        Detalle = "La fecha de inicio no puede ser mayor a la fecha fin" 
                    });
                }
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

        [HttpPost("Registrar")]
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

        [HttpGet("detalle/{id}")]
        [ProducesResponseType(typeof(SiniestroResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerSiniestroPorId(int id)
        {
            try
            {
                
                if (id <= 0)
                {
                    return BadRequest(new { error = "El ID debe ser mayor a 0" });
                }
                
                var consulta = new ObtenerSiniestroPorId(id);
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