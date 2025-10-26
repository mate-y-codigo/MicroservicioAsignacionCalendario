using MicroservicioAsignacionCalendario.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroservicioAsignacionCalendario.Infrastructure.Persistence.EntityConfigurations
{
    public class EjercicioRegistroConfiguration : IEntityTypeConfiguration<EjercicioRegistro>
    {
        public void Configure(EntityTypeBuilder<EjercicioRegistro> builder)
        {
            builder.ToTable("EjercicioRegistro");

            builder.HasKey(er => er.Id);
            builder.Property(er => er.Series).HasColumnType("int").IsRequired();
            builder.Property(er => er.Repeticiones).HasColumnType("int").IsRequired();
            builder.Property(er => er.Peso).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(er => er.Completado).HasColumnType("bool").HasDefaultValue(false);

            builder.HasOne<SesionRealizada>(er => er.SesionRealizada)
                .WithMany(sr => sr.EjerciciosRegistrados)
                .HasForeignKey(er => er.IdSesionRealizada)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
