using Inventory.DB.References;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DB.Contexts
{
    public class UnitOMContext : DbContext
    {
        public DbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }

        public UnitOMContext(DbContextOptions<UnitOMContext> options)
           : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UnitOfMeasurement>(entity =>
            {
                entity.ToTable("UnitOfMeasurement");
                entity.HasKey(e => e.id);
            });
        }
    }
}
