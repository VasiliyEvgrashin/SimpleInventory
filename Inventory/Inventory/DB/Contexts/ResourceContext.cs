using Microsoft.EntityFrameworkCore;

namespace Inventory.DB.Contexts
{
    public class ResourceContext : DbContext
    {
        public DbSet<Resource> Resource { get; set; }

        public ResourceContext(DbContextOptions<ResourceContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Resource>(entity =>
            {
                entity.ToTable("Resource");
                entity.HasKey(e => e.id);
            });
        }
    }
}
