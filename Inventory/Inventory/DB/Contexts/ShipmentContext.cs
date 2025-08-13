using Microsoft.EntityFrameworkCore;

namespace Inventory.DB.Contexts
{
    public class ShipmentContext : DbContext
    {
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<ResShipment> ResShipment { get; set; }

        public ShipmentContext(DbContextOptions<ShipmentContext> options)
           : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("Shipment");
                entity.HasKey(e => e.id);
            });

            modelBuilder.Entity<ResShipment>(entity =>
            {
                entity.ToTable("ResShipment");
                entity.HasKey(e => e.id);
                entity.HasOne(e => e.Shipment)
                    .WithMany(e => e.ResShipment)
                    .HasForeignKey(e => e.shipmentid);
            });
        }
    }
}
