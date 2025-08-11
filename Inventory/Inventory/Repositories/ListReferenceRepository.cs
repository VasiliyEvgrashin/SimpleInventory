using Inventory.DB;
using Inventory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories
{
    public class ListReferenceRepository : IListReferenceRepository
    {
        IFactoryProvider _provider;

        public ListReferenceRepository(IFactoryProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<T>> Get<T>() where T : class
        {
            await using (var context = await _provider.CreateContext<T>())
            {
                return await context
                    .Set<T>()
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
}
