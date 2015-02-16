using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Anthoro.NetExensions.Extensions
{
    internal static class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object @object)
        {
            var dictionary = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
            if (@object != null)
            {
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(@object))
                {
                    dictionary.Add(propertyDescriptor.Name.Replace("_", "-"), propertyDescriptor.GetValue(@object));
                }
            }
            return dictionary;
        }
    }
}