using KalumNotas.Entities;
using Microsoft.EntityFrameworkCore;

namespace KalumNotas.KalumBDContext
{
    public class KalumNotasBDContext : DbContext
    {
        public KalumNotasBDContext(DbContextOptions<KalumNotasBDContext> options)
            : base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alumno>().HasKey( x => new {x.AlumnoId} );
            modelBuilder.Entity<AsignacionAlumno>().HasKey( x => new {x.AsignacionId} );
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<AsignacionAlumno> AsignacionAlumnos { get; set; }
        public DbSet<DetalleNota> DetalleNota { get; set; }
        public DbSet<Modulo> Modulo { get; set; }
        public DbSet<Seminario> Seminario { get; set; }
        public DbSet<DetalleActividad> DetalleActividad { get; set; }
    }
}