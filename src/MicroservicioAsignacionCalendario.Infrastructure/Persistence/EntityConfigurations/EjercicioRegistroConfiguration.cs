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

            // Referencias
            builder.Property(er => er.IdSesionRealizada).HasColumnType("uuid").IsRequired();
            builder.Property(er => er.IdEjercicioSesion).HasColumnType("uuid").IsRequired();
            builder.Property(er => er.IdEjercicio).HasColumnType("uuid").IsRequired();

            // Snapshot: Ejercicio Sesion
            builder.Property(er => er.SeriesObjetivo).HasColumnType("int").IsRequired();
            builder.Property(er => er.RepeticionesObjetivo).HasColumnType("int").IsRequired();
            builder.Property(er => er.PesoObjetivo).HasPrecision(6, 2).IsRequired();
            builder.Property(er => er.DescansoObjetivo).HasColumnType("int").IsRequired();
            builder.Property(er => er.OrdenEjercicio).HasColumnType("int").IsRequired();

            // Snapshots Ejercicio
            builder.Property(er => er.NombreEjercicio).HasMaxLength(100).IsRequired(false);
            builder.Property(er => er.NombreMusculo).HasMaxLength(50).IsRequired(false);
            builder.Property(er => er.NombreGrupoMuscular).HasMaxLength(50).IsRequired(false);
            builder.Property(er => er.NombreCategoria).HasMaxLength(25).IsRequired(false);
            builder.Property(er => er.UrlDemoEjercicio).HasColumnType("text").IsRequired(false);

            // Datos Reales
            builder.Property(er => er.Series).HasColumnType("int").IsRequired();
            builder.Property(er => er.Repeticiones).HasColumnType("int").IsRequired();
            builder.Property(er => er.Peso).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(er => er.FechaRealizacion).IsRequired();
        }
    }
}
