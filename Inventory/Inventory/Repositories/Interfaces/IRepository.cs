using Inventory.DB.References;

namespace Inventory.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetAsync<T>() where T : class;
        Task<T> GetAsync<T>(int id) where T : class;
        Task<int> InsertAsync<T>(T value) where T : class;
        Task<bool> UpdateAsync<T>(T value) where T : BaseReference;
    }
}
