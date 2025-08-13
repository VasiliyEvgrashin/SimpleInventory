using Inventory.Models;

namespace Inventory.DB
{
    public class Receipt
    {
        public int id { get; set; }
        public string number { get; set; }
        public DateTime createdate { get; set; }
        public IList<ResReceipt> ResReceipt { get; set; } = new List<ResReceipt>();

        public Receipt() { }
        public Receipt(ReceiptEditModel model)
        {
            number = model.number;
            createdate = model.createdate;
            foreach (var item in model.items) {
                ResReceipt.Add(new ResReceipt(item));
            }
        }

        internal void UpdateFrom(ReceiptEditModel item)
        {
            number = item.number;
            createdate = item.createdate;
        }
    }
}
