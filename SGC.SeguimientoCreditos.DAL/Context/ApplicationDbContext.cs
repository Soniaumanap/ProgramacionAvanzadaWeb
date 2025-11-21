using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.DAL.Contexto
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SolicitudCredito> SolicitudesCredito { get; set; }
        public DbSet<TrackingGestion> TrackingsGestion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<SolicitudCredito>().ToTable("SolicitudCredito");
            modelBuilder.Entity<TrackingGestion>().ToTable("TrackingGestion");
        }
    }
}
