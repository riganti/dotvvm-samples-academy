namespace System.Xml
{
    public static class XmlNameTableExtensions
    {
        public static string GetOrAdd(this XmlNameTable nameTable, string name)
        {
            if (name == null)
            {
                return null;
            }
            var existingValue = nameTable.Get(name);
            if (existingValue != null)
            {
                return existingValue;
            }

            return nameTable.Add(name);
        }
    }
}