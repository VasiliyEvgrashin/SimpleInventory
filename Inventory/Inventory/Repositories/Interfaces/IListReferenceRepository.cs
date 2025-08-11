namespace Inventory.Repositories.Interfaces
{
    public interface IListReferenceRepository
    {
        Task<IEnumerable<T>> Get<T>() where T : class;
    }
}
