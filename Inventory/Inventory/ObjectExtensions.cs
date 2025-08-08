using System.Reflection;

namespace Inventory
{
    public static class ObjectExtensions
    {
        public static string GetEntryID(this Type obj)
        {
            TypeInfo props = obj.GetTypeInfo();
            EntryIDAttribute? attr = props.GetCustomAttribute<EntryIDAttribute>();
            if (attr == null) return string.Empty;
            return attr.IDProperty;
        }
    }

    public class EntryIDAttribute : Attribute
    {
        public string IDProperty { get; set; }

        public EntryIDAttribute(string id)
        {
            IDProperty = id;
        }
    }
}
