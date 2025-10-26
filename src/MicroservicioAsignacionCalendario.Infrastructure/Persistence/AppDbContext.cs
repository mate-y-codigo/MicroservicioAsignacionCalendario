using MicroservicioAsignacionCalendario.Domain.Entities;
using MicroservicioAsignacionCalendario.Infrastructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace MicroservicioAsignacionCalendario.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<RecordPersonal> RecordPersonal { get; set; }
        public DbSet<SesionRealizada> SesionRealizada { get; set; }
        public DbSet<EventoCalendario> EventoCalendario { get; set; }
        public DbSet<AlumnoPlan> AlumnoPlan { get; set; }
        public DbSet<EjercicioRegistro> EjercicioRegistro { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlumnoPlanConfiguration());
            modelBuilder.ApplyConfiguration(new RecordPersonalConfiguration());
            modelBuilder.ApplyConfiguration(new SesionRealizadaConfiguration());
            modelBuilder.ApplyConfiguration(new EjercicioRegistroConfiguration());
            modelBuilder.ApplyConfiguration(new EventoCalendarioConfiguration());
        }
    }
}
