using Inventory.Models;

namespace Inventory.DB
{
    public class ResShipment
    {
        public int id { get; set; }
        public int shipmentid { get; set; }
        public int resourceid { get; set; }
        public int unitofmeasurementid { get; set; }
        public int count { get; set; }
        public Shipment Shipment { get; set; }

        public ResShipment() { }
        public ResShipment(ShipmentEditItemModel item)
        {
            resourceid = item.resourceid;
            unitofmeasurementid = item.unitofmeasurementid;
            count = item.count;
        }

        internal void UpdateFrom(ShipmentEditItemModel res)
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
            if (obj is ShipmentEditItemModel)
            {
                ShipmentEditItemModel other = (ShipmentEditItemModel)obj;
                return
                    this.resourceid == other.resourceid &&
                    this.unitofmeasurementid == other.unitofmeasurementid &&
                    this.count == other.count;
            }
            if (obj is ResShipment)
            {
                ResShipment other = (ResShipment)obj;
                return this.id == other.id;
            }
            return false;
        }
    }
}
