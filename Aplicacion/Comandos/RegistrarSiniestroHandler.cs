using MediatR;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Excepciones;

namespace Aplicacion.Comandos
{
    public class RegistrarSiniestroHandler : IRequestHandler<RegistrarSiniestro, int>
    {
        private readonly ITransaccionalidad _transaccionalidad;

        public RegistrarSiniestroHandler(ITransaccionalidad transaccionalidad)
        {
            _transaccionalidad = transaccionalidad;
        }

        public async Task<int> Handle(RegistrarSiniestro request, CancellationToken cancellationToken)
        {
            var ciudad = await _transaccionalidad.Ciudades.GetByIdAsync(request.CiudadId);
            if (ciudad == null)
                throw new DomainException($"La ciudad con ID {request.CiudadId} no existe");
            
            var tipoSiniestro = await _transaccionalidad.TiposSiniestro.GetByIdAsync(request.TipoSiniestroId);
            if (tipoSiniestro == null)
                throw new DomainException($"El tipo de siniestro con ID {request.TipoSiniestroId} no existe");
            
            if (request.FechaHora > DateTime.Now.AddHours(1)) 
                throw new DomainException("La fecha no puede ser futura");
            
            var siniestro = new Siniestro
            {
                FechaHora = request.FechaHora,
                CiudadId = request.CiudadId,
                TipoSiniestroId = request.TipoSiniestroId,
                VehiculosInvolucrados = request.VehiculosInvolucrados,
                NumeroVictimas = request.NumeroVictimas,
                Descripcion = request.Descripcion,
                FechaCreacion = DateTime.UtcNow
            };
            
            await _transaccionalidad.Siniestros.AddAsync(siniestro);
            await _transaccionalidad.CompleteAsync();
            
            return siniestro.Id;
        }
    }
}