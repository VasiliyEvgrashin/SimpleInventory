using Inventory.Core;
using Inventory.DB;
using Inventory.Helpers.Interfaces;
using Inventory.Models;
using Inventory.Repositories.Interfaces;

namespace Inventory.Helpers
{
    public class CheckBalanceHelper : ICheckBalanceHelper
    {
        ICheckBalanceRepository _checkBalanceRepository;

        public CheckBalanceHelper(ICheckBalanceRepository checkBalanceRepository)
        {
            _checkBalanceRepository = checkBalanceRepository;
        }

        public async Task<bool> CheckBalance(ShipmentEditModel model)
        {
            if (model.issign)
            {
                await _checkBalanceRepository.SignShipment(model.id, false);
                return true;
            }
            if (CalculateBalance(
                _checkBalanceRepository.GetShippingResources(model),
                await _checkBalanceRepository.GetReceiptedResources(),
                await _checkBalanceRepository.GetShippedResources()
                ))
            {
                await _checkBalanceRepository.SignShipment(model.id, true);
                return true;
            }
            return false;
        }

        bool CalculateBalance(
            IEnumerable<IGrouping<BalanceCompositeKey, ShipmentEditItemModel>> shippingresources,
            IEnumerable<IGrouping<BalanceCompositeKey, ResReceipt>> receiptedresources,
            IEnumerable<IGrouping<BalanceCompositeKey, ResShipment>> shippedresources)
        {
            bool result = true;
            foreach (var res in shippingresources)
            {
                var ship = res.Sum(s => s.count);
                var balancekey = receiptedresources.FirstOrDefault(f => f.Key == res.Key);
                var rec = balancekey == null ? 0 : balancekey.Sum(s => s.count);
                var approvedkey = shippedresources.FirstOrDefault(f => f.Key == res.Key);
                var approved = approvedkey == null ? 0 : approvedkey.Sum(s => s.count);
                result &= (rec - approved) - ship > 0;
            }
            return result;
        }
    }
}
