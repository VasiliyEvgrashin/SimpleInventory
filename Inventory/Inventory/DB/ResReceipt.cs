using Inventory.Models;

namespace Inventory.DB
{
    public class ResReceipt
    {
        public int id { get; set; }
        public int receiptid { get; set; }
        public int resourceid { get; set; }
        public int unitofmeasurementid { get; set; }
        public int count { get; set; }
        public Receipt Receipt { get; set; }

        public ResReceipt() { }
        public ResReceipt(ReceiptEditItemModel item)
        {
            resourceid = item.resourceid;
            unitofmeasurementid = item.unitofmeasurementid;
            count = item.count;
        }

        internal void UpdateFrom(ReceiptEditItemModel res)
        {
            resourceid = res.resourceid;
            unitofmeasurementid = res.unitofmeasurementid;
            count = res.count;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is ReceiptEditItemModel)
            {
                ReceiptEditItemModel other = (ReceiptEditItemModel)obj;
                return
                    this.resourceid == other.resourceid &&
                    this.unitofmeasurementid == other.unitofmeasurementid &&
                    this.count == other.count;
            }
            if (obj is ResReceipt)
            {
                ResReceipt other = (ResReceipt)obj;
                return this.id == other.id;
            }
            return false;
        }
    }
}
