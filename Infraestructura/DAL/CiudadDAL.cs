using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Infraestructura.DAL
{
    public class CiudadDAL : ICiudad
    {
        private readonly ApplicationDbContext _context;

        public CiudadDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Ciudad?> GetByIdAsync(int id)
        {
            return await _context.Ciudades
                .Include(c => c.Departamento)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Ciudad>> GetAllAsync()
        {
            return await _context.Ciudades
                .Include(c => c.Departamento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Ciudad>> GetByDepartamentoIdAsync(int departamentoId)
        {
            return await _context.Ciudades
                .Where(c => c.DepartamentoId == departamentoId)
                .ToListAsync();
        }

        public async Task AddAsync(Ciudad ciudad)
        {
            await _context.Ciudades.AddAsync(ciudad);
        }

        public void Update(Ciudad ciudad)
        {
            _context.Ciudades.Update(ciudad);
        }

        public void Delete(Ciudad ciudad)
        {
            _context.Ciudades.Remove(ciudad);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Ciudades.AnyAsync(c => c.Id == id);
        }
    }
}