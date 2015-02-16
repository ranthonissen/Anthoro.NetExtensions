using System.Collections.Generic;

namespace Anthoro.NetExensions.Extensions
{
    public static class IListExtensions
    {
        public static bool IsIn<T>(this T subject, IList<T> list)
        {
            return list.Contains(subject);
        }
    }
}