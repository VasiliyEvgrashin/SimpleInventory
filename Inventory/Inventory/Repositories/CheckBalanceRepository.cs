using Inventory.Core;
using Inventory.DB;
using Inventory.DB.Contexts;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories
{
    public class CheckBalanceRepository : ICheckBalanceRepository
    {
        IDbContextFactory<ShipmentContext> _shipmentfabric;
        IDbContextFactory<ReceiptContext> _receiptfabric;

        public CheckBalanceRepository(
            IDbContextFactory<ShipmentContext> shipmentfabric,
            IDbContextFactory<ReceiptContext> receiptfabric
            )
        {
            _shipmentfabric = shipmentfabric;
            _receiptfabric = receiptfabric;
        }

        public async Task<bool> SignShipment(int shipmentid, bool value)
        {
            await using (ShipmentContext shipmentcontext = await _shipmentfabric.CreateDbContextAsync())
            {
                var exist = await shipmentcontext.Shipment.FirstOrDefaultAsync(f => f.id == shipmentid);
                if (exist != null)
                {
                    exist.issign = value;
                    await shipmentcontext.SaveChangesAsync();
                }
            }
            return true;
        }

        public async Task<IEnumerable<IGrouping<BalanceCompositeKey, ResReceipt>>> GetReceiptedResources()
        {
            await using (ReceiptContext context = await _receiptfabric.CreateDbContextAsync())
            {
                return (await context
                    .ResReceipt
                    .AsNoTracking()
                    .ToListAsync())
                    .GroupBy(g => new BalanceCompositeKey(g.resourceid, g.unitofmeasurementid));
            }
        }

        public async Task<IEnumerable<IGrouping<BalanceCompositeKey, ResShipment>>> GetShippedResources()
        {
            await using (ShipmentContext shipmentcontext = await _shipmentfabric.CreateDbContextAsync())
            {
                return (await shipmentcontext
                    .ResShipment
                    .Include(i => i.Shipment)
                    .Where(w => w.Shipment.issign)
                    .AsNoTracking()
                    .ToListAsync())
                .GroupBy(g => new BalanceCompositeKey(g.resourceid, g.unitofmeasurementid));
            }
        }

        public IEnumerable<IGrouping<BalanceCompositeKey, ShipmentEditItemModel>> GetShippingResources(ShipmentEditModel model)
        {
            return model
                .items
                .GroupBy(g => new BalanceCompositeKey(g.resourceid, g.unitofmeasurementid));
        }
    }
}
