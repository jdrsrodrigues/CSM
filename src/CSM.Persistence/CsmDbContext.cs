using CSM.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSM.Persistence
{
    public class CsmDbContext : DbContext
    {
        public DbSet<Usuario> tbUsuario { get; set; }
        public DbSet<Alerta> tbAlerta { get; set; }
        public CsmDbContext(DbContextOptions<CsmDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("tbUsuario");
            modelBuilder.Entity<Alerta>().ToTable("tbAlerta");

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Alertas)
                .WithOne(a => a.Usuario)
                .HasForeignKey(a => a.IdUsuario);
        }
    }
}
