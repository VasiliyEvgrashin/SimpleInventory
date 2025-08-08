using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Inventory.DB
{
    public interface IFactoryProvider
    {
        Task<DbContext> CreateContext<T>() where T : class;
    }
}
