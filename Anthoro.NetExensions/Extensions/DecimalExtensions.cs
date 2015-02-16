using System;

namespace Anthoro.NetExensions.Extensions
{
    public static class DecimalExtensions
    {
        public static int RoundToIntWithTwoDecimals(this decimal value)
        {
            if (value > int.MaxValue || value < int.MinValue)
            {
                throw new InvalidCastException("Cannot cast " + value + " to an int. This value is out of range.");
            }
            return (int)Math.Ceiling(Math.Truncate(value * 100) / 100);
        }

        public static int RoundToInt(this decimal value)
        {
            if (value > int.MaxValue || value < int.MinValue)
            {
                throw new InvalidCastException("Cannot cast " + value + " to an int. This value is out of range.");
            }
            return (int)Math.Ceiling(value);
        }
    }
}