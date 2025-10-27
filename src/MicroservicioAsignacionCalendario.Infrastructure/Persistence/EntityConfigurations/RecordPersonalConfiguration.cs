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
            builder.Property(rp => rp.Id).HasColumnType("uuid");
            builder.Property(rp => rp.IdAlumno).IsRequired().HasColumnType("uuid");
            builder.Property(rp => rp.IdEjercicio).IsRequired().HasColumnType("uuid");
            builder.Property(rp => rp.PesoMax).IsRequired().HasColumnType("decimal(10,2)");
            builder.Property(rp => rp.Series).IsRequired().HasColumnType("int");
            builder.Property(rp => rp.Repeticiones).IsRequired().HasColumnType("int");
            builder.Property(rp => rp.FechaRegistro)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();
        }
    }
}
