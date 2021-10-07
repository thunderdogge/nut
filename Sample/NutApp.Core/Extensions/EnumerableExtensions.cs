namespace NutApp.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<TSource>(this TSource[] source)
        {
            return source == null || source.Length == 0;
        }
    }
}