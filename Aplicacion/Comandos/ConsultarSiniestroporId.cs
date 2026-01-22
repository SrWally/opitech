using MediatR;
using Aplicacion.DTO;

namespace Aplicacion.Comandos
{
    public class ObtenerSiniestroPorId : IRequest<SiniestroResponseDTO?>
    {
        public int Id { get; }

        public ObtenerSiniestroPorId(int id)
        {
            Id = id;
        }
    }
}