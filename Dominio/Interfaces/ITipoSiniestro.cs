using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ITipoSiniestro
    {
        Task<TipoSiniestro?> GetByIdAsync(int id);
        Task<IEnumerable<TipoSiniestro>> GetAllAsync();
        Task<IEnumerable<TipoSiniestro>> GetByNombreAsync(string nombre);
        Task AddAsync(TipoSiniestro tipoSiniestro);
        void Update(TipoSiniestro tipoSiniestro);
        void Delete(TipoSiniestro tipoSiniestro);
        Task<bool> ExistsAsync(int id);
    }
}