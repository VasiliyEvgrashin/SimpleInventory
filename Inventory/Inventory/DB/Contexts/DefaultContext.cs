using Inventory.DB.References;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DB.Contexts
{
    public class DefaultContext : DbContext
    {
        string nvarchar;
        public DbSet<Client> Client { get; set; }
        public DbSet<UnitOfMeasurement> UnitOfMeasurement { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<Receipt> Receipt { get; set; }
        public DbSet<ResReceipt> ResReceipt { get; set; }

        public DefaultContext(DbContextOptions<DefaultContext> options)
           : base(options)
        {
            nvarchar = $"nvarchar({Constants.MAX_FIELD_LENGTH})";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.name).HasColumnType(nvarchar);
                entity.Property(e => e.address).HasColumnType(nvarchar);
            });

            modelBuilder.Entity<UnitOfMeasurement>(entity =>
            {
                entity.ToTable("UnitOfMeasurement");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.name).HasColumnType(nvarchar);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("Resource");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.name).HasColumnType(nvarchar);
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("Receipt");
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.Property(e => e.number).HasColumnType(nvarchar);
                entity.HasKey(e => e.id);
            });

            modelBuilder.Entity<ResReceipt>(entity =>
            {
                entity.ToTable("ResReceipt");
                entity.HasKey(e => e.id);
                entity.Property(e => e.id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.Receipt)
                    .WithMany(e => e.ResReceipt)
                    .HasForeignKey(e => e.receiptid);
            });
        }
    }
}
