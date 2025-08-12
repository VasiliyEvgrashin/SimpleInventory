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

        public async Task<IEnumerable<ReceiptListModel>> Get(DateTime datefrom, DateTime dateto)
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

        public async Task<ReceiptEditModel> Get(int id)
        {
            await using (ReceiptContext context = await _fabric.CreateDbContextAsync())
            {
                Receipt? item = await context
                    .Receipt
                    .Include(i => i.ResReceipt)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.id == id);
                return new ReceiptEditModel(item);
            }
        }

        public async Task<ReceiptEditModel> UpSert(ReceiptEditModel model)
        {
            Receipt? item = null;
            await using (ReceiptContext context = await _fabric.CreateDbContextAsync())
            {
                item = await context
                    .Receipt
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.id == model.id);
            }
            return item == null ? await Insert(model) : await Update(model);
        }

        async Task<ReceiptEditModel> Update(ReceiptEditModel model)
        {
            throw new NotImplementedException();
        }

        async Task<ReceiptEditModel> Insert(ReceiptEditModel model)
        {
            throw new NotImplementedException();
        }
    }
}
