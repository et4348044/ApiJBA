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

            // Las relaciones mediante Fluent API y configuraciones de tablas se agregarán aquí paso a paso
        }
    }
}
