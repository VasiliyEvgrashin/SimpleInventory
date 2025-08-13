using Inventory.DB;

namespace Inventory.Models
{
    public class ShipmentEditModel
    {
        public int id { get; set; }
        public string number { get; set; }
        public DateTime createdate { get; set; }
        public int clientid { get; set; }
        public bool issign { get; set; }
        public IList<ShipmentEditItemModel> items { get; set; } = new List<ShipmentEditItemModel>();

        public ShipmentEditModel() { }
        public ShipmentEditModel(Shipment item)
        {
            if (item == null)
            {
                return;
            }
            id = item.id;
            number = item.number;
            createdate = item.createdate;
            clientid = item.clientid;
            issign = item.issign;
            foreach (var it in item.ResShipment)
            {
                items.Add(new ShipmentEditItemModel(it));
            }
        }
    }

    public class ShipmentEditItemModel
    {
        public int id { get; set; }
        public int resourceid { get; set; }
        public int unitofmeasurementid { get; set; }
        public int count { get; set; }

        public ShipmentEditItemModel() { }
        public ShipmentEditItemModel(ResShipment it)
        {
            id = it.id;
            resourceid = it.resourceid;
            unitofmeasurementid = it.unitofmeasurementid;
            count = it.count;
        }
    }
}
