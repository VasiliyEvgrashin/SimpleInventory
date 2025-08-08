namespace Inventory.DB
{
    [EntryID("id")]
    public class Resource
    {
        public int id {  get; set; }
        public string name { get; set; }
        public bool isactive { get; set; }
    }
}
