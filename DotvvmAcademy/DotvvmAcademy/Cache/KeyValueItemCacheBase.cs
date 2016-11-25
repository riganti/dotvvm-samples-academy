using System;
using System.Diagnostics;

namespace DotvvmAcademy.Cache
{
    public abstract class KeyValueItemCacheBase<T>
    {
        protected TimeSpan ExpirationSpan { get; set; }
        protected Func<object, T> UpdateFunc { get; set; }
        protected Func<DateTime> DateTimeNowFactory { get; set; }

        protected KeyValueItemCacheBase()
        {
            //todo
            DateTimeNowFactory = () => DateTime.UtcNow;
            ExpirationSpan = TimeSpan.FromMinutes(15);
        }

        protected DateTime LastUpdate { get; set; } = DateTime.MinValue;

        protected T GetValue<TClass>(string key = null, object obj  = null)
        {
            CheckExpiration<TClass>(obj);
            return Cache.Get<TClass, T>(key);
        }

        protected void CheckExpiration<TClass>(object obj = null)
        {
            if (ExpirationSpan == TimeSpan.Zero)
            {
                return;
            }

            if (UpdateFunc == null)
            {
                Debug.WriteLine($"The UpdateFunc for {typeof(TClass).FullName} is not set! Cache invalidation skipped!");
                return;
            }
            if (DateTimeNowFactory() - LastUpdate > ExpirationSpan)
            {
                Debug.WriteLine($"Cache with name {typeof(TClass).FullName} was updated!");
                SetValue<TClass>(UpdateFunc(obj));
            }
        }
        

        protected void SetValue<TClass>(T value, string key = null)
        {
            LastUpdate = DateTimeNowFactory();
            Cache.Set<TClass, T>(value, key);
        }

        public string GetKeyCore<TClass>()
        {
            return typeof(TClass).FullName + "|" + typeof(T).FullName;
        }

    }
}