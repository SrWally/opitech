using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ISiniestro
    {

        Task<Siniestro?> GetByIdAsync(int id);
        Task<IEnumerable<Siniestro>> GetAllAsync();
        Task AddAsync(Siniestro siniestro);
        void Update(Siniestro siniestro);
        void Delete(Siniestro siniestro);
        Task<bool> ExistsAsync(int id);

        Task<(IEnumerable<Siniestro> Items, int TotalCount)> GetPagedAsync(
            int? departamentoId, 
            DateTime? desde, 
            DateTime? hasta,
            int pageNumber,
            int pageSize);

        Task<bool> ExistsByFechaCiudadTipoAsync(DateTime fechaHora, int ciudadId, int tipoSiniestroId);
        Task<IEnumerable<Siniestro>> GetByCiudadIdAsync(int ciudadId);
        Task<int> CountByFiltersAsync(int? departamentoId = null, DateTime? desde = null, DateTime? hasta = null, int? tipoSiniestroId = null);
        Task<IEnumerable<Siniestro>> GetByFechaRangeAsync(DateTime desde, DateTime hasta);
        Task<IEnumerable<Siniestro>> GetByTipoSiniestroIdAsync(int tipoSiniestroId);
    }
}