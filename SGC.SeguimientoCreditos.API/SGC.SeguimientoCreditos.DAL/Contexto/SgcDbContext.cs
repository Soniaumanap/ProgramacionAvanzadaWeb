using Microsoft.EntityFrameworkCore;
using SGC.SeguimientoCreditos.DAL.Entidades;

namespace SGC.SeguimientoCreditos.DAL.Contexto
{
    public class SgcDbContext : DbContext
    {
        public SgcDbContext(DbContextOptions<SgcDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SolicitudCredito> SolicitudesCredito { get; set; }
        public DbSet<TrackingGestion> TrackingsGestion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CLIENTE
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Identificacion)
                      .IsUnique();

                entity.Property(e => e.Identificacion)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Telefono)
                      .HasMaxLength(20);

                entity.Property(e => e.Email)
                      .HasMaxLength(100);
            });

            // USUARIO
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Identificacion).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Identificacion)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Password)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Rol)
                      .IsRequired();
            });

            // SOLICITUD CREDITO
            modelBuilder.Entity<SolicitudCredito>(entity =>
            {
                entity.ToTable("SolicitudesCredito");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Monto)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Cliente)
                      .WithMany(c => c.Solicitudes)
                      .HasForeignKey(e => e.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // TRACKING GESTION
            modelBuilder.Entity<TrackingGestion>(entity =>
            {
                entity.ToTable("TrackingsGestion");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Usuario)
                      .WithMany(u => u.Trackings)
                      .HasForeignKey(e => e.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
