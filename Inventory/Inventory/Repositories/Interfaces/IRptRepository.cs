using Inventory.Models;

namespace Inventory.Repositories.Interfaces
{
    public interface IRptRepository
    {
        Task<IEnumerable<ReceiptListModel>> Get(DateTime datefrom, DateTime dateto);
        Task<ReceiptEditModel> Get(int id);
        Task<ReceiptEditModel> UpSert(ReceiptEditModel model);
    }
}
