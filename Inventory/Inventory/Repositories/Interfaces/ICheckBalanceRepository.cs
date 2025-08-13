using Inventory.Core;
using Inventory.DB;
using Inventory.Models;
using System.Linq.Expressions;

namespace Inventory.Repositories.Interfaces
{
    public interface ICheckBalanceRepository
    {
        Task<bool> SignShipment(int shipmentid, bool value);
        IEnumerable<IGrouping<BalanceCompositeKey, ShipmentEditItemModel>> GetShippingResources(ShipmentEditModel model);
        Task<IEnumerable<IGrouping<BalanceCompositeKey, ResReceipt>>> GetReceiptedResources();
        Task<IEnumerable<IGrouping<BalanceCompositeKey, ResShipment>>> GetShippedResources();
        Task<IEnumerable<IGrouping<BalanceCompositeKey, ResReceipt>>> GetReceiptedResources(Expression<Func<ResReceipt, bool>> filter);
        Task<IEnumerable<IGrouping<BalanceCompositeKey, ResShipment>>> GetShippedResources(Expression<Func<ResShipment, bool>> filter);
    }
}
