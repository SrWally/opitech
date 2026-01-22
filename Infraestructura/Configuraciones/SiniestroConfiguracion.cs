using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dominio.Entidades;

namespace Infraestructura.Configuraciones
{
    public class SiniestroConfiguracion : IEntityTypeConfiguration<Siniestro>
    {
        public void Configure(EntityTypeBuilder<Siniestro> builder)
        {
            builder.ToTable("Siniestros");
            
            builder.HasKey(s => s.Id);
            
            builder.Property(s => s.FechaHora)
                .IsRequired();
            
            builder.Property(s => s.CiudadId)
                .IsRequired();
            
            builder.Property(s => s.TipoSiniestroId)
                .IsRequired();
            
            builder.Property(s => s.VehiculosInvolucrados)
                .IsRequired();
            
            builder.Property(s => s.NumeroVictimas)
                .IsRequired();
            
            builder.Property(s => s.Descripcion)
                .HasMaxLength(1000);
            
            builder.Property(s => s.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            builder.HasOne(s => s.Ciudad)
                .WithMany(c => c.Siniestros)
                .HasForeignKey(s => s.CiudadId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(s => s.TipoSiniestro)
                .WithMany(t => t.Siniestros)
                .HasForeignKey(s => s.TipoSiniestroId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasIndex(s => s.FechaHora);
            builder.HasIndex(s => s.CiudadId);
            builder.HasIndex(s => s.TipoSiniestroId);
        }
    }
}