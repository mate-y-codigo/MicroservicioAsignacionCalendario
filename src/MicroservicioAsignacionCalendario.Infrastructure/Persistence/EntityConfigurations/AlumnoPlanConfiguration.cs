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

            // Referencias
            builder.Property(ap => ap.IdAlumno).HasColumnType("uuid").IsRequired();
            builder.Property(ap => ap.IdPlanEntrenamiento).HasColumnType("uuid").IsRequired();
            builder.Property(ap => ap.IdSesionARealizar).HasColumnType("uuid").IsRequired();

            // Snapshot: Plan de Entrenamiento
            builder.Property(ap => ap.NombrePlan).HasMaxLength(100).IsRequired();
            builder.Property(ap => ap.DescripcionPlan).HasColumnType("text").IsRequired(false);

            // Otros
            builder.Property(ap => ap.Estado).HasConversion<string>();
            builder.Property(ap => ap.Notas).HasColumnType("text").IsRequired(false);
            builder.Property(ap => ap.IntervaloDiasDescanso).HasColumnType("int").IsRequired();
            builder.Property(ap => ap.FechaInicio).IsRequired();
            builder.Property(ap => ap.FechaFin).IsRequired();

            // Relaciones
            builder.HasMany(ap => ap.SesionesRealizadas)
                  .WithOne(sr => sr.AlumnoPlan)
                  .HasForeignKey(sr => sr.IdAlumnoPlan)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ap => ap.EventosCalendarios)
                  .WithOne(ec => ec.AlumnoPlan)
                  .HasForeignKey(ec => ec.IdAlumnoPlan)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}