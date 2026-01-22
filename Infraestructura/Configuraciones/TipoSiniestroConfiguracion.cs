using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dominio.Entidades;

namespace Infraestructura.Configuraciones
{
    public class TipoSiniestroConfiguracion : IEntityTypeConfiguration<TipoSiniestro>
    {
        public void Configure(EntityTypeBuilder<TipoSiniestro> builder)
        {
            builder.ToTable("TiposSiniestro");
            
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(t => t.Descripcion)
                .HasMaxLength(500);
            
            builder.HasIndex(t => t.Nombre)
                .IsUnique();
            
        }
    }
}