using Inventory.DB;
using Inventory.DB.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories
{
    public static class RepositoryExtention
    {
        public static WebApplicationBuilder BindDBContexts(this WebApplicationBuilder builder)
        {
            var connectionStringInventory = builder.Configuration.GetConnectionString("Inventory");
            builder.Services.AddDbContextFactory<DefaultContext>(options =>
                options.UseSqlServer(connectionStringInventory)
                );
            builder.Services.AddDbContextFactory<ClientContext>(options =>
                options.UseSqlServer(connectionStringInventory)
                );
            builder.Services.AddDbContextFactory<UnitOMContext>(options =>
                options.UseSqlServer(connectionStringInventory)
                );
            builder.Services.AddDbContextFactory<ResourceContext>(options =>
                options.UseSqlServer(connectionStringInventory)
                );
            
            builder.Services.AddScoped<IFactoryProvider, FactoryProvider>();
            builder.Services.AddScoped<IRepository, Repository>();
            return builder;
        }

        public static WebApplication InitDefaultContext(this WebApplication app)
        {
            using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DefaultContext>();
                context.Database.EnsureCreated();
            }
            return app;
        }
    }
}
