using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modelos;

namespace Datos
{
    public partial class ProductDBContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        public virtual DbSet<Producto> Productos { get; set; }

    }
}
