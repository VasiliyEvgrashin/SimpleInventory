using Inventory.DB;
using Inventory.DB.Contexts;
using Inventory.DB.References;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories
{
    public class RptRepository : IRptRepository
    {
        IDbContextFactory<ReceiptContext> _fabric;
        IListReferenceRepository _listReferenceRepository;

        public RptRepository(
            IDbContextFactory<ReceiptContext> fabric,
            IListReferenceRepository listReferenceRepository
            )
        {
            _fabric = fabric;
            _listReferenceRepository = listReferenceRepository;
        }

        public async Task<IEnumerable<ReceiptListModel>> GetAsync(DateTime datefrom, DateTime dateto)
        {
            IEnumerable<Receipt> receipts = null;
            await using (ReceiptContext context = await _fabric.CreateDbContextAsync())
            {
                receipts = await context
                    .Receipt
                    .Include(i => i.ResReceipt)
                    .Where(w => w.createdate.Date >= datefrom && w.createdate.Date <= dateto)
                    .AsNoTracking()
                    .ToListAsync();
            }
            IEnumerable<Resource> resources = await _listReferenceRepository.Get<Resource>();
            IEnumerable<UnitOfMeasurement> units = await _listReferenceRepository.Get<UnitOfMeasurement>();
            IList<ReceiptListModel> result = new List<ReceiptListModel>();
            if (receipts == null)
            {
                return result;
            }
            Parallel.ForEach(receipts, (receipt) =>
            {
                result.Add(new ReceiptListModel(receipt, resources, units));
            });
            return result;
        }
    }
}
