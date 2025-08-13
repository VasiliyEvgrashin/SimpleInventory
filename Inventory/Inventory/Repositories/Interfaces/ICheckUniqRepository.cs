
namespace Inventory.Repositories.Interfaces
{
    public interface ICheckUniqRepository
    {
        Task<bool> CheckUniqueReceipt(string value);
        Task<bool> CheckUniqueShipment(string value);
    }
}
