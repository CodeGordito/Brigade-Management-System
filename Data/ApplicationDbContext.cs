using GestorBrigadasComunitarias.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorBrigadasComunitarias.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Voluntario> Voluntarios { get; set; }
        public DbSet<Zona> Zonas { get; set; }
        public DbSet<Jornada> Jornadas { get; set; }
        public DbSet<Participacion> Participaciones { get; set; }
        public DbSet<Material> Materiales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Participacion>()
                .HasOne(p => p.Voluntario)
                .WithMany(v => v.Participaciones)
                .HasForeignKey(p => p.VoluntarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Participacion>()
                .HasOne(p => p.Jornada)
                .WithMany(j => j.Participaciones)
                .HasForeignKey(p => p.JornadaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Jornada)
                .WithMany(j => j.Materiales)
                .HasForeignKey(m => m.JornadaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}