using Microsoft.EntityFrameworkCore;
using SGC.DAL.Entidades;

namespace SGC.DAL
{
    public class SgcDbContext : DbContext
    {
        public SgcDbContext(DbContextOptions<SgcDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<SolicitudCredito> SolicitudesCredito { get; set; }
        public DbSet<TrackingGestion> TrackingsGestion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolicitudCredito>()
                .Property(x => x.Id)
                .UseIdentityColumn(seed: 11550, increment: 1);

            base.OnModelCreating(modelBuilder);
        }
    }
}
