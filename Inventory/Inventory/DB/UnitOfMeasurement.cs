namespace Inventory.DB
{
    [EntryID("id")]
    public class UnitOfMeasurement
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isactive { get; set; }
    }
}
