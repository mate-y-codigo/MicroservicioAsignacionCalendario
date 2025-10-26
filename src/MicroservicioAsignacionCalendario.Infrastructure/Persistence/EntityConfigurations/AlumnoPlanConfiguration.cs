using MicroservicioAsignacionCalendario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroservicioAsignacionCalendario.Infrastructure.Persistence.EntityConfigurations
{
    public class AlumnoPlanConfiguration : IEntityTypeConfiguration<AlumnoPlan>
    {
        public void Configure(EntityTypeBuilder<AlumnoPlan> builder)
        {
            builder.ToTable("AlumnoPlan");

            builder.HasKey(ap => ap.Id);
            builder.Property(ap => ap.Estado).HasDefaultValue(Estado.Activo);
            builder.Property(ap => ap.Notas).HasColumnType("text");
            builder.Property(ap => ap.IntervaloDiasDescanso).HasColumnType("int").IsRequired();
            builder.Property(ap => ap.FechaInicio)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
            builder.Property(ap => ap.FechaFin)
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            builder.Property(ap => ap.IdAlumno).HasColumnType("uuid").IsRequired();
            builder.Property(ap => ap.IdPlanEntrenamiento).HasColumnType("uuid").IsRequired();
            builder.Property(ap => ap.IdSesionActual).HasColumnType("uuid").IsRequired();
        }
    }
}