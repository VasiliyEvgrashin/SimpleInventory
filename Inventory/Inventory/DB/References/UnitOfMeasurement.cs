namespace Inventory.DB.References
{
    [EntryID("id")]
    public class UnitOfMeasurement : BaseReference
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isactive { get; set; }

        public override BaseReference Update(BaseReference source)
        {
            UnitOfMeasurement transform = source as UnitOfMeasurement;
            if (transform != null)
            {
                name = transform.name;
            }
            return this;
        }
    }
}
