using Inventory.DB.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Inventory.DB
{
    public class FactoryProvider : IFactoryProvider
    {
        readonly IServiceProvider _serviceProvider;

        public FactoryProvider(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<DbContext> CreateContext<T>() where T : class
        {
            return typeof(T).Name switch
            {
                "Client" => await _serviceProvider.GetService<IDbContextFactory<ClientContext>>().CreateDbContextAsync(),
                "UnitOfMeasurement" => await _serviceProvider.GetService<IDbContextFactory<UnitOMContext>>().CreateDbContextAsync(),
                "Resource" => await _serviceProvider.GetService<IDbContextFactory<ResourceContext>>().CreateDbContextAsync(),
                _ => await _serviceProvider.GetService<IDbContextFactory<DefaultContext>>().CreateDbContextAsync()
            };
        }
    }
}
