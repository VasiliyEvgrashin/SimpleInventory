namespace Inventory.DB
{
    [EntryID("id")]
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isactive { get; set; }
        public string address { get; set; }
    }
}
