using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Infraestructura.DAL
{
    public class TipoSiniestroDAL : ITipoSiniestro
    {
        private readonly ApplicationDbContext _context;

        public TipoSiniestroDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TipoSiniestro?> GetByIdAsync(int id)
        {
            return await _context.TiposSiniestro
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TipoSiniestro>> GetAllAsync()
        {
            return await _context.TiposSiniestro
                .OrderBy(t => t.Nombre)
                .ToListAsync();
        }

        public async Task<IEnumerable<TipoSiniestro>> GetByNombreAsync(string nombre)
        {
            return await _context.TiposSiniestro
                .Where(t => t.Nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task AddAsync(TipoSiniestro tipoSiniestro)
        {
            await _context.TiposSiniestro.AddAsync(tipoSiniestro);
        }

        public void Update(TipoSiniestro tipoSiniestro)
        {
            _context.TiposSiniestro.Update(tipoSiniestro);
        }

        public void Delete(TipoSiniestro tipoSiniestro)
        {
            _context.TiposSiniestro.Remove(tipoSiniestro);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.TiposSiniestro.AnyAsync(t => t.Id == id);
        }
    }
}