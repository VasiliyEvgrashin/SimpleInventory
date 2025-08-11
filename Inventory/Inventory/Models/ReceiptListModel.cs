using Inventory.DB;
using Inventory.DB.References;

namespace Inventory.Models
{
    public class ReceiptListModel
    {
        public ReceiptListModel(Receipt s, IEnumerable<Resource> resources, IEnumerable<UnitOfMeasurement> units)
        {
            id = s.id;
            number = s.number;
            createdate = s.createdate;
            if (s.ResReceipt != null)
            {
                foreach (ResReceipt item in s.ResReceipt)
                {
                    Resource? resource = resources.FirstOrDefault(f => f.id == item.resourceid);
                    UnitOfMeasurement? unit = units.FirstOrDefault(f => f.id == item.unitofmeasurementid);
                    items.Add(new ReceiptListItemModel()
                    {
                        count = item.count,
                        name = resource == null ? "" : resource.name,
                        uofm = unit == null ? "" : unit.name
                    });
                }
            }
        }

        public int id { get; set; }
        public string number { get; set; }
        public DateTime createdate { get; set; }
        public IList<ReceiptListItemModel> items { get; set; } = new List<ReceiptListItemModel>();
    }

    public class ReceiptListItemModel
    {
        public string name { get; set; }
        public string uofm { get; set; }
        public int count { get; set; }
    }
}
