
using Inventory.DB;
using Inventory.DB.References;
using Inventory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Inventory.Repositories
{
    public class Repository : IRepository
    {
        IFactoryProvider _factoryProvider;

        public Repository(IFactoryProvider factoryProvider)
        {
            _factoryProvider = factoryProvider;
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

        public async Task<int> InsertAsync<T>(T value) where T : class
        {
            await using (var _context = await _factoryProvider.CreateContext<T>())
            {
                _context.Set<T>().Add(value);
                _context.Entry(value).State = EntityState.Added;
                await _context.SaveChangesAsync();
                return GetID<T>(value);
            }
        }

        public async Task<bool> UpdateAsync<T>(T value) where T : BaseReference
        {
            int id = GetID<T>(value);
            var lambda = GetExpressionID<T>(id);
            await using (var _context = await _factoryProvider.CreateContext<T>())
            {
                var it = await _context.Set<T>().FirstOrDefaultAsync(lambda);
                if (it == null)
                {
                    return false;
                }
                it.Update(value);
                _context.Entry(it).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
        }

        protected int GetID<T>(T value) where T: class
        {
            var tof = typeof(T);
            string pn = tof.GetEntryID();
            PropertyInfo? pi = tof.GetProperty(pn);
            if (pi == null)
            {
                return 0;
            }
            object? obj = pi.GetValue(value, null);
            if (obj == null)
            {
                return 0;
            }
            if (obj is int)
            {
                return (int)obj;
            }
            return 0;
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
