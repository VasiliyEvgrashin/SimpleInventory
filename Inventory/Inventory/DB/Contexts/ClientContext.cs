using Inventory.DB.References;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DB.Contexts
{
    public class ClientContext : DbContext
    {
        public DbSet<Client> Client { get; set; }

        public ClientContext(DbContextOptions<ClientContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");
                entity.HasKey(e => e.id);
            });
        }
    }
}
