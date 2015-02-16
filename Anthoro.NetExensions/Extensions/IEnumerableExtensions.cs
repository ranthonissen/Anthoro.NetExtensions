using System;
using System.Collections.Generic;

namespace Anthoro.NetExensions.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void Update<T>(this IEnumerable<T> source, params Action<T>[] updates)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (updates == null)
                throw new ArgumentNullException("updates");

            foreach (T item in source)
            {
                foreach (Action<T> update in updates)
                {
                    update(item);
                }
            }
        }

        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}