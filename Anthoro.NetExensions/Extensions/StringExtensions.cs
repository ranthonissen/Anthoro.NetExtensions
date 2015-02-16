using System;
using System.Collections.Generic;

namespace Anthoro.NetExensions.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static string Join(this string[] values, string separator)
        {
            return string.Join(separator, values);
        }

        public static string Join(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }

        public static string FormatWith(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static T ToEnum<T>(this string value, T defaultValue)
        {
            if (!value.HasValue())
            {
                return defaultValue;
            }
            T result;
            try
            {
                result = (T)Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentException)
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// Wraps the string in a javascript tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string AsJavaScript(this string value)
        {
            return string.Format(@"<script type=""text/javascript""> {0} </script>", value);
        }

        /// <summary>
        /// Wraps the value in a jQuery ready function.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string AsJQueryReady(this string value)
        {
            return string.Format("$(function() {{ {0} }});", value).AsJavaScript();
        }
    }
}