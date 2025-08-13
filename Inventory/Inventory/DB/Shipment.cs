using Inventory.Models;

namespace Inventory.DB
{
    public class Shipment
    {
        public int id { get; set; }
        public string number { get; set; }
        public int clientid { get; set; }
        public DateTime createdate { get; set; }
        public bool issign {  get; set; }
        public bool isactive { get; set; }
        public IList<ResShipment> ResShipment { get; set; } = new List<ResShipment>();

        public Shipment() { }
        public Shipment(ShipmentEditModel model)
        {
            number = model.number;
            createdate = model.createdate;
            clientid = model.clientid;
            foreach (var item in model.items)
            {
                ResShipment.Add(new ResShipment(item));
            }
        }

        internal void UpdateFrom(ShipmentEditModel item)
        {
            number = item.number;
            createdate = item.createdate;
            clientid = item.clientid;
            issign = item.issign;
        }
    }
}
