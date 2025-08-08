namespace Inventory.Repositories
{
    public interface IRepository
    {
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAsync<T>() where T : class;
        Task<T> GetAsync<T>(int id) where T : class;
        Task<int> InsertAsync<T>(T value) where T : class;
        Task UpdateAsync<T>(T value) where T : class;
    }
}
