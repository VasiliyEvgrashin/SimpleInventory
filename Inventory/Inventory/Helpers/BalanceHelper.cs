using Inventory.Core;
using Inventory.DB;
using Inventory.DB.References;
using Inventory.Helpers.Interfaces;
using Inventory.Models;
using Inventory.Repositories.Interfaces;

namespace Inventory.Helpers
{
    public class BalanceHelper : IBalanceHelper
    {
        IExpressionHelper _expressionHelper;
        ICheckBalanceRepository _checkBalanceRepository;
        IListReferenceRepository _listReferenceRepository;

        public BalanceHelper(
            IExpressionHelper expressionHelper,
            ICheckBalanceRepository checkBalanceRepository,
            IListReferenceRepository listReferenceRepository)
        {
            _expressionHelper = expressionHelper;
            _checkBalanceRepository = checkBalanceRepository;
            _listReferenceRepository = listReferenceRepository;
        }

        public async Task<IEnumerable<BalanceModel>> Get(BalanceFilter filter)
        {
            var receipted = await _checkBalanceRepository.GetReceiptedResources(_expressionHelper.GetFilter<ResReceipt>(filter));
            var shipped = await _checkBalanceRepository.GetShippedResources(_expressionHelper.GetFilter<ResShipment>(filter));
            IEnumerable<BalanceComposite> joined = from g1 in receipted
                         join g2 in shipped on g1.Key equals g2.Key into gj
                         from subg2 in gj.DefaultIfEmpty()
                         select new BalanceComposite(g1.Key, g1.Select(s => s.count).Sum(), subg2 == null ? 0 : subg2.Select(s => s.count).Sum());
            return await MapResult(joined);
        }

        async Task<IEnumerable<BalanceModel>> MapResult(IEnumerable<BalanceComposite> joined)
        {
            IEnumerable<Resource> resources = await _listReferenceRepository.Get<Resource>();
            IEnumerable<UnitOfMeasurement> units = await _listReferenceRepository.Get<UnitOfMeasurement>();
            return joined.Select(s => new BalanceModel()
            {
                resource = resources.FirstOrDefault(f => f.id == s.key.resourceid)?.name,
                unit = units.FirstOrDefault(f => f.id == s.key.unitofmeasurementid)?.name,
                count = s.receipted - s.shipped
            });
        }
    }
}
