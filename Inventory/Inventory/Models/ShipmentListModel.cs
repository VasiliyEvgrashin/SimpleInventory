using Inventory.DB;
using Inventory.DB.References;

namespace Inventory.Models
{
    public class ShipmentListModel
    {
        public int id { get; set; }
        public string number { get; set; }
        public DateTime createdate { get; set; }
        public string client { get; set; }
        public bool issign { get; set; }
        public IList<ShipmentListItemModel> items { get; set; } = new List<ShipmentListItemModel>();

        public ShipmentListModel(Shipment s, IEnumerable<Resource> resources, IEnumerable<UnitOfMeasurement> units, IEnumerable<Client> clients)
        {
            id = s.id;
            number = s.number;
            createdate = s.createdate;
            issign = s.issign;
            Client? cl = clients.FirstOrDefault(f => f.id == s.clientid);
            client = cl == null ? "" : cl.name;
            if (s.ResShipment != null)
            {
                foreach (ResShipment item in s.ResShipment)
                {
                    Resource? resource = resources.FirstOrDefault(f => f.id == item.resourceid);
                    UnitOfMeasurement? unit = units.FirstOrDefault(f => f.id == item.unitofmeasurementid);
                    items.Add(new ShipmentListItemModel()
                    {
                        count = item.count,
                        name = resource == null ? "" : resource.name,
                        uofm = unit == null ? "" : unit.name
                    });
                }
            }
        }
    }

    public class ShipmentListItemModel
    {
        public string name { get; set; }
        public string uofm { get; set; }
        public int count { get; set; }
    }
}
