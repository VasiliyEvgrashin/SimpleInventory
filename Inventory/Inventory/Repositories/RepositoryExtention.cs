using Inventory.DB;
using Inventory.DB.Contexts;
using Inventory.Helpers;
using Inventory.Helpers.Interfaces;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
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
            builder.Services.AddDbContextFactory<ReceiptContext>(options =>
                options.UseSqlServer(connectionStringInventory)
                );
            builder.Services.AddDbContextFactory<ShipmentContext>(options =>
                options.UseSqlServer(connectionStringInventory)
                );
            return builder;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IExpressionHelper, ExpressionHelper>();
            services.AddScoped<IFactoryProvider, FactoryProvider>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IListReferenceRepository, ListReferenceRepository>();
            services.AddScoped<ICheckBalanceRepository, CheckBalanceRepository>();
            services.AddScoped<ICheckBalanceHelper, CheckBalanceHelper>();
            services.AddScoped<IReceiptRepository, ReceiptRepository>();
            services.AddScoped<IShipmentRepository, ShipmentRepository>();
            services.AddScoped<IBalanceHelper, BalanceHelper>();
            services.AddScoped<ICheckUniqRepository, CheckUniqRepository>();
            return services;
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

        public static IEnumerable<ResReceipt> CheckInsert(this IEnumerable<ResReceipt> destination, IEnumerable<ReceiptEditItemModel> source)
        {
            IList<ResReceipt> result = new List<ResReceipt>();
            foreach (var sourceItem in source) {
                ResReceipt? exist = destination.FirstOrDefault(f => f.id == sourceItem.id);
                if ( exist == null)
                {
                    result.Add(new ResReceipt(sourceItem));
                }
            }
            return result;
        }

        public static void UpdateItems(this IEnumerable<ResReceipt> destination, IEnumerable<ReceiptEditItemModel> source)
        {
            foreach (var sourceItem in source)
            {
                ResReceipt? exist = destination.FirstOrDefault(f => f.id == sourceItem.id);
                if (exist != null && !exist.Equals(sourceItem))
                {
                    exist.UpdateFrom(sourceItem);
                }
            }
        }

        public static IEnumerable<ResReceipt> CheckDelete(this IEnumerable<ResReceipt> destination, IEnumerable<ReceiptEditItemModel> source)
        {
            IList<ResReceipt> result = new List<ResReceipt>();
            foreach (var destinationitem in destination)
            {
                var exist = source.FirstOrDefault(f => f.id == destinationitem.id);
                if (exist == null)
                {
                    result.Add(destinationitem);
                }
            }
            return result;
        }

        public static IEnumerable<ResShipment> CheckInsert(this IEnumerable<ResShipment> destination, IEnumerable<ShipmentEditItemModel> source)
        {
            IList<ResShipment> result = new List<ResShipment>();
            foreach (var sourceItem in source)
            {
                ResShipment? exist = destination.FirstOrDefault(f => f.id == sourceItem.id);
                if (exist == null)
                {
                    result.Add(new ResShipment(sourceItem));
                }
            }
            return result;
        }

        public static void UpdateItems(this IEnumerable<ResShipment> destination, IEnumerable<ShipmentEditItemModel> source)
        {
            foreach (var sourceItem in source)
            {
                ResShipment? exist = destination.FirstOrDefault(f => f.id == sourceItem.id);
                if (exist != null && !exist.Equals(sourceItem))
                {
                    exist.UpdateFrom(sourceItem);
                }
            }
        }

        public static IEnumerable<ResShipment> CheckDelete(this IEnumerable<ResShipment> destination, IEnumerable<ShipmentEditItemModel> source)
        {
            IList<ResShipment> result = new List<ResShipment>();
            foreach (var destinationitem in destination)
            {
                var exist = source.FirstOrDefault(f => f.id == destinationitem.id);
                if (exist == null)
                {
                    result.Add(destinationitem);
                }
            }
            return result;
        }
    }
}
