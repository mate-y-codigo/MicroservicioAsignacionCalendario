using MicroservicioAsignacionCalendario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroservicioAsignacionCalendario.Infrastructure.Persistence.EntityConfigurations
{
    public class EventoCalendarioConfiguration : IEntityTypeConfiguration<EventoCalendario>
    {
        public void Configure(EntityTypeBuilder<EventoCalendario> builder)
        {
            builder.ToTable("EventoCalendario");

            builder.HasKey(ec => ec.Id);

            // Referencias
            builder.Property(ec => ec.IdAlumnoPlan).HasColumnType("uuid").IsRequired();
            builder.Property(ec => ec.IdSesionEntrenamiento).HasColumnType("uuid").IsRequired();

            // Snapshots: Sesion de entrenamiento
            builder.Property(sr => sr.NombreSesion).HasMaxLength(100).IsRequired();

            // Otros
            builder.Property(ec => ec.Estado).HasConversion<string>();
            builder.Property(ec => ec.Notas).HasColumnType("text").IsRequired(false);
            builder.Property(ec => ec.FechaProgramada).IsRequired();
        }
    }
}
