namespace Inventory.DB.References
{
    [EntryID("id")]
    public class Client : BaseReference
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isactive { get; set; }
        public string address { get; set; }

        public override BaseReference Update(BaseReference source)
        {
            Client transform = source as Client;
            if (transform != null)
            {
                name = transform.name;
                address = transform.address;
            }
            return this;
        }
    }
}
