namespace Inventory.Models
{
    public record BalanceFilter(IList<int> resources, IList<int> units);
}
