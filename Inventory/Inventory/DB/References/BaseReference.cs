namespace Inventory.DB.References
{
    public abstract class BaseReference
    {
        protected BaseReference() { }

        public abstract BaseReference Update(BaseReference source);
    }
}
