using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ICiudad
    {
        Task<Ciudad?> GetByIdAsync(int id);
        Task<IEnumerable<Ciudad>> GetAllAsync();
        Task<IEnumerable<Ciudad>> GetByDepartamentoIdAsync(int departamentoId);
        Task AddAsync(Ciudad ciudad);
        void Update(Ciudad ciudad);
        void Delete(Ciudad ciudad);
        Task<bool> ExistsAsync(int id);
    }
}