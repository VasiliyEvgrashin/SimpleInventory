using Inventory.Models;

namespace Inventory.Repositories.Interfaces
{
    public interface IShipmentRepository
    {
        Task<IEnumerable<ShipmentListModel>> Get(DateTime datefrom, DateTime dateto);
        Task<ShipmentEditModel> Get(int id);
        Task<ShipmentEditModel> UpSert(ShipmentEditModel model);
    }
}
