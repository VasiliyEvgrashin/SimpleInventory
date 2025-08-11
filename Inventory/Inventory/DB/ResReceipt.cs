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
    }
}
