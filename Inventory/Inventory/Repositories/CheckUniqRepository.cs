using Inventory.DB.Contexts;
using Inventory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories
{
    public class CheckUniqRepository : ICheckUniqRepository
    {
        IDbContextFactory<ShipmentContext> _shipmentfabric;
        IDbContextFactory<ReceiptContext> _receiptfabric;

        public CheckUniqRepository(
            IDbContextFactory<ShipmentContext> shipmentfabric,
            IDbContextFactory<ReceiptContext> receiptfabric
            )
        {
            _shipmentfabric = shipmentfabric;
            _receiptfabric = receiptfabric;
        }

        public async Task<bool> CheckUniqueReceipt(string value)
        {
            await using (ReceiptContext receiptcontext = await _receiptfabric.CreateDbContextAsync())
            {
                var exist = await receiptcontext
                    .Receipt
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.number == value);
                return exist == null;
            }
        }

        public async Task<bool> CheckUniqueShipment(string value)
        {
            await using (ShipmentContext shipmentcontext = await _shipmentfabric.CreateDbContextAsync())
            {
                var exist = await shipmentcontext
                    .Shipment
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.number == value);
                return exist == null;
            }
        }
    }
}
