using Inventory.Models;
using System.Linq.Expressions;

namespace Inventory.Helpers.Interfaces
{
    public interface IExpressionHelper
    {
        Expression<Func<T, bool>> GetFilter<T>(BalanceFilter filter);
    }
}
