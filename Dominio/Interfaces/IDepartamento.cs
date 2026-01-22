using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IDepartamento
    {
        Task<Departamento?> GetByIdAsync(int id);
        Task<IEnumerable<Departamento>> GetAllAsync();
        Task<IEnumerable<Departamento>> GetByNombreAsync(string nombre);
        Task AddAsync(Departamento departamento);
        void Update(Departamento departamento);
        void Delete(Departamento departamento);
        Task<bool> ExistsAsync(int id);
    }
}