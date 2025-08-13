using Inventory.Models;

namespace Inventory.Helpers.Interfaces
{
    public interface IBalanceHelper
    {
        Task<IEnumerable<BalanceModel>> Get(BalanceFilter filter);
    }
}
