using Inventory.DB;
using Inventory.DB.Contexts;
using Inventory.DB.References;
using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        IDbContextFactory<ShipmentContext> _shipmentfabric;
        IListReferenceRepository _listReferenceRepository;

        public ShipmentRepository(
            IDbContextFactory<ShipmentContext> shipmentfabric,
            IListReferenceRepository listReferenceRepository
            )
        {
            _shipmentfabric = shipmentfabric;
            _listReferenceRepository = listReferenceRepository;
        }

        public async Task<IEnumerable<ShipmentListModel>> Get(DateTime datefrom, DateTime dateto)
        {
            IEnumerable<Shipment> shipments = null;
            await using (ShipmentContext context = await _shipmentfabric.CreateDbContextAsync())
            {
                shipments = await context
                    .Shipment
                    .Include(i => i.ResShipment)
                    .Where(w => w.createdate.Date >= datefrom && w.createdate.Date <= dateto)
                    .AsNoTracking()
                    .ToListAsync();
            }
            IEnumerable<Resource> resources = await _listReferenceRepository.Get<Resource>();
            IEnumerable<UnitOfMeasurement> units = await _listReferenceRepository.Get<UnitOfMeasurement>();
            IEnumerable<Client> clients = await _listReferenceRepository.Get<Client>();
            IList<ShipmentListModel> result = new List<ShipmentListModel>();
            if (shipments == null)
            {
                return result;
            }
            Parallel.ForEach(shipments, (shipment) =>
            {
                result.Add(new ShipmentListModel(shipment, resources, units, clients));
            });
            return result;
        }

        public async Task<ShipmentEditModel> Get(int id)
        {
            await using (ShipmentContext context = await _shipmentfabric.CreateDbContextAsync())
            {
                Shipment? item = await context
                    .Shipment
                    .Include(i => i.ResShipment)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.id == id);
                return new ShipmentEditModel(item);
            }
        }

        public async Task<ShipmentEditModel> UpSert(ShipmentEditModel source)
        {
            await using (ShipmentContext context = await _shipmentfabric.CreateDbContextAsync())
            {
                Shipment? exist = await context
                    .Shipment
                    .Include(i => i.ResShipment)
                    .FirstOrDefaultAsync(f => f.id == source.id);
                if (exist == null)
                {
                    Shipment newitem = new Shipment(source);
                    await context.Shipment.AddAsync(newitem);
                    await context.SaveChangesAsync();
                    return new ShipmentEditModel(newitem);
                }
                else
                {
                    exist.UpdateFrom(source);
                    context.Entry(exist).State = EntityState.Modified;
                    IEnumerable<ResShipment> insertitems = exist.ResShipment.CheckInsert(source.items);
                    exist.ResShipment.UpdateItems(source.items);
                    IEnumerable<ResShipment> deleteitems = exist.ResShipment.CheckDelete(source.items);
                    foreach (ResShipment item in insertitems)
                    {
                        exist.ResShipment.Add(item);
                    }
                    foreach (ResShipment item in deleteitems)
                    {
                        exist.ResShipment.Remove(item);
                    }
                    await context.SaveChangesAsync();
                    return new ShipmentEditModel(exist);
                }
            }
        }
    }
}
