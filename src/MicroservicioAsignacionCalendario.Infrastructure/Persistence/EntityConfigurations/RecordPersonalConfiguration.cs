using MicroservicioAsignacionCalendario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroservicioAsignacionCalendario.Infrastructure.Persistence.EntityConfigurations
{
    public class RecordPersonalConfiguration : IEntityTypeConfiguration<RecordPersonal>
    {
        public void Configure(EntityTypeBuilder<RecordPersonal> builder)
        {
            builder.ToTable("RecordPersonal");

            builder.HasKey(rp => rp.Id);

            // Referencias
            builder.Property(rp => rp.IdAlumno).HasColumnType("uuid").IsRequired();
            builder.Property(rp => rp.IdEjercicio).HasColumnType("uuid").IsRequired();
            
            // Snapshot: Ejercicio
            builder.Property(er => er.NombreEjercicio).HasMaxLength(100).IsRequired();
            builder.Property(er => er.NombreGrupoMuscular).HasMaxLength(50).IsRequired();
            
            // Otros
            builder.Property(rp => rp.PesoMax).HasPrecision(6, 2).IsRequired();
            builder.Property(rp => rp.Series).IsRequired().HasColumnType("int");
            builder.Property(rp => rp.Repeticiones).IsRequired().HasColumnType("int");
            builder.Property(rp => rp.FechaRegistro).IsRequired();
            builder.Property(rp => rp.Calculo1RM).HasPrecision(6, 2).IsRequired();

            // Indice para rendimiento
            builder.HasIndex(rp => new { rp.IdAlumno, rp.IdEjercicio })
                  .IsUnique();
        }
    }
}
