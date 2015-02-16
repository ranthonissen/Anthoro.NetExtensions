namespace Anthoro.NetExensions.Extensions
{
    public static class BooleanExtensions
    {
        public static string ToJavaScript(this bool value)
        {
            return value
                ? "true"
                : "false";
        }
    }
}