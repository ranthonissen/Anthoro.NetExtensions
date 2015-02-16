using System;
using System.Runtime.Caching;

namespace Anthoro.NetExensions
{
    public class Cache
    {
        private static readonly MemoryCache MemoryCache;

        static Cache()
        {
            MemoryCache = MemoryCache.Default;
        }

        public static T Get<T>(string key, Func<T> function)
        {
            return Get(key, function, new TimeSpan(8, 0, 0));
        }

        public static T Get<T>(string key, Func<T> function, TimeSpan timeToCache)
        {
            if (!MemoryCache.Contains(key))
            {
                MemoryCache.Add(key, function.Invoke(), DateTime.Now.Add(timeToCache));
            }

            return (T)MemoryCache.Get(key);
        }
    }
}