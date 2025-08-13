using Inventory.Core;
using Inventory.DB;
using Inventory.Models;

namespace Inventory.Helpers.Interfaces
{
    public interface ICheckBalanceHelper
    {
        Task<bool> CheckBalance(ShipmentEditModel model);
    }
}
