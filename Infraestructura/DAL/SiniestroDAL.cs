using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Infraestructura.DAL
{
    public class SiniestroDAL : ISiniestro
    {
        private readonly ApplicationDbContext _context;

        public SiniestroDAL(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Siniestro?> GetByIdAsync(int id)
        {
            return await _context.Siniestros
                .Include(s => s.Ciudad)
                    .ThenInclude(c => c.Departamento)
                .Include(s => s.TipoSiniestro)  
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Siniestro>> GetAllAsync()
        {
            return await _context.Siniestros
                .Include(s => s.Ciudad)
                    .ThenInclude(c => c.Departamento)
                .Include(s => s.TipoSiniestro) 
                .OrderByDescending(s => s.FechaHora)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Siniestro> Items, int TotalCount)> GetPagedAsync(
            int? departamentoId, 
            DateTime? desde, 
            DateTime? hasta,
            int pageNumber,
            int pageSize)
        {
            var query = _context.Siniestros
                .Include(s => s.Ciudad)
                    .ThenInclude(c => c.Departamento)
                .Include(s => s.TipoSiniestro) 
                .AsQueryable();

            if (departamentoId.HasValue)
            {
                query = query.Where(s => s.Ciudad!.DepartamentoId == departamentoId.Value);
            }

            if (desde.HasValue)
            {
                query = query.Where(s => s.FechaHora >= desde.Value);
            }

            if (hasta.HasValue)
            {
                query = query.Where(s => s.FechaHora <= hasta.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(s => s.FechaHora)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<bool> ExistsByFechaCiudadTipoAsync(DateTime fechaHora, int ciudadId, int tipoSiniestroId)
        {
            return await _context.Siniestros
                .AnyAsync(s => s.FechaHora.Date == fechaHora.Date &&
                              s.CiudadId == ciudadId &&
                              s.TipoSiniestroId == tipoSiniestroId);
        }

        public async Task<IEnumerable<Siniestro>> GetByCiudadIdAsync(int ciudadId)
        {
            return await _context.Siniestros
                .Include(s => s.Ciudad)
                    .ThenInclude(c => c.Departamento)
                .Include(s => s.TipoSiniestro)
                .Where(s => s.CiudadId == ciudadId)
                .OrderByDescending(s => s.FechaHora)
                .ToListAsync();
        }

        public async Task<int> CountByFiltersAsync(
            int? departamentoId = null, 
            DateTime? desde = null, 
            DateTime? hasta = null,
            int? tipoSiniestroId = null)
        {
            var query = _context.Siniestros.AsQueryable();

            if (departamentoId.HasValue)
            {
                query = query.Where(s => s.Ciudad!.DepartamentoId == departamentoId.Value);
            }

            if (desde.HasValue)
            {
                query = query.Where(s => s.FechaHora >= desde.Value);
            }

            if (hasta.HasValue)
            {
                query = query.Where(s => s.FechaHora <= hasta.Value);
            }

            if (tipoSiniestroId.HasValue)
            {
                query = query.Where(s => s.TipoSiniestroId == tipoSiniestroId.Value);
            }

            return await query.CountAsync();
        }

        public async Task<IEnumerable<Siniestro>> GetByFechaRangeAsync(DateTime desde, DateTime hasta)
        {
            return await _context.Siniestros
                .Include(s => s.Ciudad)
                    .ThenInclude(c => c.Departamento)
                .Include(s => s.TipoSiniestro)
                .Where(s => s.FechaHora >= desde && s.FechaHora <= hasta)
                .OrderByDescending(s => s.FechaHora)
                .ToListAsync();
        }

        public async Task<IEnumerable<Siniestro>> GetByTipoSiniestroIdAsync(int tipoSiniestroId)
        {
            return await _context.Siniestros
                .Include(s => s.Ciudad)
                    .ThenInclude(c => c.Departamento)
                .Include(s => s.TipoSiniestro)
                .Where(s => s.TipoSiniestroId == tipoSiniestroId)
                .OrderByDescending(s => s.FechaHora)
                .ToListAsync();
        }

        public async Task AddAsync(Siniestro siniestro)
        {
            await _context.Siniestros.AddAsync(siniestro);
        }

        public void Update(Siniestro siniestro)
        {
            _context.Siniestros.Update(siniestro);
        }

        public void Delete(Siniestro siniestro)
        {
            _context.Siniestros.Remove(siniestro);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Siniestros.AnyAsync(s => s.Id == id);
        }
    }
}