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
            builder.Property(sr => sr.Id).HasColumnType("uuid");
            builder.Property(sr => sr.IdSesionEntrenamiento).IsRequired().HasColumnType("uuid");
            builder.Property(sr => sr.IdPlanAlumno).IsRequired().HasColumnType("uuid");
            builder.Property(sr => sr.Estado).IsRequired().HasColumnType("int").HasDefaultValue(Estado.Activo);
            builder.Property(sr => sr.FechaRealizacion)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            builder.HasOne(sr => sr.AlumnoPlan)
            .WithMany(ap => ap.SesionesRealizadas)
            .HasForeignKey(sr => sr.IdPlanAlumno)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
