using Inventory.DB;
using Inventory.DB.References;

namespace Inventory.Models
{
    public class ReceiptEditModel
    {
        public ReceiptEditModel(Receipt item)
        {
            if (item == null)
            {
                return;
            }
            id = item.id;
            number = item.number;
            createdate = item.createdate;
            foreach(var it in item.ResReceipt)
            {
                items.Add(new ReceiptEditItemModel(it));
            }
        }

        public int id { get; set; }
        public string number { get; set; }
        public DateTime createdate { get; set; }
        public IList<ReceiptEditItemModel> items { get; set; } = new List<ReceiptEditItemModel>();
    }

    public class ReceiptEditItemModel
    {
        public ReceiptEditItemModel(ResReceipt it)
        {
            id = it.id;
            resourceid = it.resourceid;
            unitofmeasurementid = it.unitofmeasurementid;
            count = it.count;
        }

        public int id { get; set; }
        public int resourceid { get; set; }
        public int unitofmeasurementid { get; set; }
        public int count { get; set; }
    }
}
