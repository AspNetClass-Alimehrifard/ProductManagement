using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Entities;

namespace ProductManagement.Application.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ProjectDbContext()
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(p => p.Description)
                      .HasMaxLength(500);

                entity.Property(p => p.Price)
                      .HasPrecision(18, 2);

                entity.Property(p => p.Category)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }
    }
}
