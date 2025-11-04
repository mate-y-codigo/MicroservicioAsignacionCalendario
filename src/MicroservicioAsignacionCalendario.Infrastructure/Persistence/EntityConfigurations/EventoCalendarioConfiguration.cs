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
            builder.Property(ec => ec.Id).HasColumnType("uuid");
            builder.Property(ec => ec.IdAlumno).IsRequired().HasColumnType("uuid");
            builder.Property(ec => ec.IdEntrenador).IsRequired().HasColumnType("uuid");
            builder.Property(ec => ec.IdSesionEntrenamiento).IsRequired().HasColumnType("uuid");
            builder.Property(ec => ec.FechaInicio)
                .HasColumnType("timestamp with time zone")
                .IsRequired();
            builder.Property(ec => ec.FechaFin)
                .HasColumnType("timestamp with time zone")
                .IsRequired();
            builder.Property(ec => ec.Estado).IsRequired().HasColumnType("int").HasDefaultValue(EstadoEvento.Programado);
            builder.Property(ec => ec.Notas).HasColumnType("text");
            builder.Property(ec => ec.FechaCreacion)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
            builder.Property(ec => ec.FechaActualizacion)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
