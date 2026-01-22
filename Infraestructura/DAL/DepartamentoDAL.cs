using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Infraestructura.DAL
{
    public class DepartamentoDAL : IDepartamento
    {
        private readonly ApplicationDbContext _context;

        public DepartamentoDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Departamento?> GetByIdAsync(int id)
        {
            return await _context.Departamentos
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Departamento>> GetAllAsync()
        {
            return await _context.Departamentos
                .OrderBy(d => d.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Departamento>> GetByNombreAsync(string nombre)
        {
            return await _context.Departamentos
                .Where(d => d.Nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task AddAsync(Departamento departamento)
        {
            await _context.Departamentos.AddAsync(departamento);
        }

        public void Update(Departamento departamento)
        {
            _context.Departamentos.Update(departamento);
        }

        public void Delete(Departamento departamento)
        {
            _context.Departamentos.Remove(departamento);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Departamentos.AnyAsync(d => d.Id == id);
        }
    }
}