using Dominio.Interfaces;

namespace Infraestructura.DAL
{
    public class Transaccionalidad : ITransaccionalidad
    {
        private readonly ApplicationDbContext _context;
        private bool _disposed = false;

        public ISiniestro Siniestros { get; }
        public ICiudad Ciudades { get; }
        public IDepartamento Departamentos { get; }
        public ITipoSiniestro TiposSiniestro { get; }

        public Transaccionalidad(
            ApplicationDbContext context,
            ISiniestro siniestros,
            ICiudad ciudades,
            IDepartamento departamentos,
            ITipoSiniestro tiposSiniestro)
        {
            _context = context;
            Siniestros = siniestros;
            Ciudades = ciudades;
            Departamentos = departamentos;
            TiposSiniestro = tiposSiniestro;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}