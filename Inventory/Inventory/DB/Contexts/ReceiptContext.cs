using Microsoft.EntityFrameworkCore;

namespace Inventory.DB.Contexts
{
    public class ReceiptContext : DbContext
    {
        public DbSet<Receipt> Receipt { get; set; }
        public DbSet<ResReceipt> ResReceipt { get; set; }

        public ReceiptContext(DbContextOptions<ReceiptContext> options)
           : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.ToTable("Receipt");
                entity.HasKey(e => e.id);
            });

            modelBuilder.Entity<ResReceipt>(entity =>
            {
                entity.ToTable("ResReceipt");
                entity.HasKey(e => e.id);
                entity.HasOne(e => e.Receipt)
                    .WithMany(e => e.ResReceipt)
                    .HasForeignKey(e => e.receiptid);
            });
        }
    }
}
