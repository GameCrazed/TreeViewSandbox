using System;
using System.Collections.Generic;

namespace TreeViewSandbox.DupesFromArtemis
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<O> ForEach<T, O>(this IEnumerable<T> enumerable, Func<T, O> predicate)
        {
            foreach (var t in enumerable)
            {
                yield return predicate(t);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> predicate)
        {
            foreach (var t in enumerable)
            {
                predicate(t);
            }
        }
    }
}
