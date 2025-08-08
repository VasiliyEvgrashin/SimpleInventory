
using Inventory.DB;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Inventory.Repositories
{
    public class Repository : IRepository
    {
        IFactoryProvider _factoryProvider;

        public Repository(IFactoryProvider factoryProvider)
        {
            _factoryProvider = factoryProvider;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAsync<T>() where T : class
        {
            await using (var _context = await _factoryProvider.CreateContext<T>())
            {
                IList<T> list = await _context
                    .Set<T>()
                    .AsNoTracking()
                    .ToListAsync();
                return list;
            }
        }

        public async Task<T> GetAsync<T>(int id) where T : class
        {
            var lambda = GetExpressionID<T>(id);
            await using (var _context = await _factoryProvider.CreateContext<T>())
            {
                var it = await _context
                    .Set<T>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(lambda);
                return it;
            }
        }

        public Task<int> InsertAsync<T>(T value) where T : class
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(T value) where T : class
        {
            throw new NotImplementedException();
        }

        protected Expression<Func<T, bool>> GetExpressionID<T>(int id) where T : class
        {
            var item = Expression.Parameter(typeof(T), "item");
            var prop = Expression.Property(item, typeof(T).GetEntryID());
            ConstantExpression soap = Expression.Constant(id);
            var equal = Expression.Equal(prop, soap);
            return Expression.Lambda<Func<T, bool>>(equal, item);
        }
    }
}
