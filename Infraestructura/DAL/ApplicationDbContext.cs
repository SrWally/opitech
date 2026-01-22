using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;

namespace Infraestructura.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Siniestro> Siniestros => Set<Siniestro>();
        public DbSet<Ciudad> Ciudades => Set<Ciudad>();
        public DbSet<Departamento> Departamentos => Set<Departamento>();
        public DbSet<TipoSiniestro> TiposSiniestro => Set<TipoSiniestro>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}