namespace Inventory.DB
{
    public class Receipt
    {
        public int id { get; set; }
        public string number { get; set; }
        public DateTime createdate {  get; set; }

        public IList<ResReceipt> ResReceipt { get; set; } = new List<ResReceipt>();
    }
}
