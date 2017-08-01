namespace DotvvmAcademy.Cache
{
    public abstract class KeyValueItemCacheBase<T>
    {
        public string GetKeyCore<TClass>()
        {
            return typeof(TClass).FullName + "|" + typeof(T).FullName;
        }

        protected T GetValue<TClass>(string key = null, object obj = null)
        {
            return Cache.Get<TClass, T>(key);
        }

        protected void SetValue<TClass>(T value, string key = null)
        {
            Cache.Set<TClass, T>(value, key);
        }
    }
}