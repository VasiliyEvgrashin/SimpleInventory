using Inventory.Models;

namespace Inventory.Repositories.Interfaces
{
    public interface IRptRepository
    {
        Task<IEnumerable<ReceiptListModel>> GetAsync(DateTime datefrom, DateTime dateto);
    }
}
