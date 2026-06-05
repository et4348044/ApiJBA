using Microsoft.EntityFrameworkCore;
using ApiJBA.Entidades;

namespace ApiJBA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Personal> Personal { get; set; }
        public DbSet<Operacion> Operaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración en cascada de la relación entre Personal y Operaciones (Auditoría)
            modelBuilder.Entity<Operacion>()
                .HasOne(o => o.Personal)
                .WithMany(p => p.Operaciones)
                .HasForeignKey(o => o.ci_p)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
