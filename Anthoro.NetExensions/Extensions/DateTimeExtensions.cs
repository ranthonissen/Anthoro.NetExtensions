using System;

namespace Anthoro.NetExensions.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime NextDay(this DateTime date)
        {
            return date.AddDays(1);
        }

        public static DateTime? NextDay(this DateTime? date)
        {
            if (date.HasValue) return date.Value.AddDays(1);
            return null;
        }

        public static DateTime PreviousDay(this DateTime date)
        {
            return date.AddDays(1);
        }

        public static DateTime? PreviousDay(this DateTime? date)
        {
            if (date.HasValue) return date.Value.AddDays(1);
            return null;
        }
    }
}