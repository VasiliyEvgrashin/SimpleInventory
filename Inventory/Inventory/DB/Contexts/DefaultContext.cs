using Microsoft.EntityFrameworkCore;

namespace Inventory.DB.Contexts
{
    public class DefaultContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }
        public DbSet<Resource> Resource { get; set; }

        public DefaultContext(DbContextOptions<DefaultContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.name).HasColumnType("nvarchar(100)");
                entity.Property(e => e.address).HasColumnType("nvarchar(100)");
            });

            modelBuilder.Entity<UnitOfMeasurement>(entity =>
            {
                entity.ToTable("UnitOfMeasurement");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.name).HasColumnType("nvarchar(100)");
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("Resource");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.name).HasColumnType("nvarchar(100)");
            });
        }
    }
}
