using System.Net;

namespace Inventory.DB.References
{
    [EntryID("id")]
    public class Resource : BaseReference
    {
        public int id {  get; set; }
        public string name { get; set; }
        public bool isactive { get; set; }

        public override BaseReference Update(BaseReference source)
        {
            Resource transform = source as Resource;
            if (transform != null)
            {
                name = transform.name;
            }
            return this;
        }
    }
}
