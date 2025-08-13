using Inventory.DB;
using Inventory.DB.Contexts;
using Inventory.DB.References;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        IDbContextFactory<ReceiptContext> _fabric;
        IListReferenceRepository _listReferenceRepository;

        public ReceiptRepository(
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

        public async Task<ReceiptEditModel> UpSert(ReceiptEditModel source)
        {
            await using (ReceiptContext context = await _fabric.CreateDbContextAsync())
            {
                Receipt? exist = await context
                    .Receipt
                    .Include(i => i.ResReceipt)
                    .FirstOrDefaultAsync(f => f.id == source.id);
                if (exist == null)
                {
                    Receipt newitem = new Receipt(source);
                    await context.Receipt.AddAsync(newitem);
                    await context.SaveChangesAsync();
                    return new ReceiptEditModel(newitem);
                } else
                {
                    exist.UpdateFrom(source);
                    context.Entry(exist).State = EntityState.Modified;
                    IEnumerable<ResReceipt> insertitems = exist.ResReceipt.CheckInsert(source.items);
                    exist.ResReceipt.UpdateItems(source.items);
                    IEnumerable<ResReceipt> deleteitems = exist.ResReceipt.CheckDelete(source.items);
                    foreach (ResReceipt item in insertitems) {
                        exist.ResReceipt.Add(item);
                    }
                    foreach (ResReceipt item in deleteitems) {
                        exist.ResReceipt.Remove(item);
                    }
                    await context.SaveChangesAsync();
                    return new ReceiptEditModel(exist);
                }
            }
        }
    }
}
