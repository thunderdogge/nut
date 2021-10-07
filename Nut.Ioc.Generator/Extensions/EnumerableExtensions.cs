using System;
using System.Collections.Generic;
using System.Linq;

namespace Nut.Ioc.Generator.Extensions
{
    public static class EnumerableExtensions
    {
        public static HashSet<TType> ToHashSet<TType>(this IEnumerable<TType> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new HashSet<TType>(source);
        }

        public static TResult[] SelectArray<TType, TResult>(this IEnumerable<TType> source, Func<TType, TResult> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(selector).ToArray();
        }
    }
}