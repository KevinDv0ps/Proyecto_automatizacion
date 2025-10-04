using Microsoft.EntityFrameworkCore;
using SigestProAPI.Models;

namespace SigestProAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Proyecto>()
                .HasOne(p => p.Gerente)
                .WithMany()
                .HasForeignKey(p => p.GerenteId);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Empleado)
                .WithMany()
                .HasForeignKey(t => t.EmpleadoId);

            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Proyecto)
                .WithMany(p => p.Tareas)
                .HasForeignKey(t => t.ProyectoId);
        }
    }
}
