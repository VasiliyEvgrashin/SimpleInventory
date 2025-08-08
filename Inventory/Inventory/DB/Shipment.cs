namespace Inventory.DB
{
    public class Shipment
    {
        public int id { get; set; }
        public string number { get; set; }
        public int clientid { get; set; }
        public DateTime createdate { get; set; }
        public bool isactive { get; set; }
    }
}
