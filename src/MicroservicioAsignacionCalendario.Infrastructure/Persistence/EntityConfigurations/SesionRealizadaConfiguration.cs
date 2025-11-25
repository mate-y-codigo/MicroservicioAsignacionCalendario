using MicroservicioAsignacionCalendario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroservicioAsignacionCalendario.Infrastructure.Persistence.EntityConfigurations
{
    public class SesionRealizadaConfiguration : IEntityTypeConfiguration<SesionRealizada>
    {
        public void Configure(EntityTypeBuilder<SesionRealizada> builder)
        {
            builder.ToTable("SesionRealizada");

            builder.HasKey(sr => sr.Id);

            // Referencias
            builder.Property(sr => sr.IdSesionEntrenamiento).HasColumnType("uuid").IsRequired();
            builder.Property(sr => sr.IdAlumnoPlan).HasColumnType("uuid").IsRequired();
            
            // Snapshot: Sesion entrenamiento
            builder.Property(sr => sr.NombreSesion).HasMaxLength(100).IsRequired();
            builder.Property(sr => sr.OrdenSesion).HasColumnType("int").IsRequired();

            // Snapshot: Alumno
            builder.Property(sr => sr.PesoCorporalAlumno).HasPrecision(5, 2);
            builder.Property(sr => sr.AlturaEnCmAlumno).HasPrecision(5, 2);

            // Otros
            builder.Property(sr => sr.Estado).HasConversion<string>();
            builder.Property(sr => sr.FechaRealizacion).IsRequired(false);

            builder.HasMany(e => e.EjerciciosRegistrados)
              .WithOne(er => er.SesionRealizada)
              .HasForeignKey(er => er.IdSesionRealizada)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
