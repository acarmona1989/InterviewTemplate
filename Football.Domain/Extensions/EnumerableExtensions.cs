using System;
using System.Collections.Generic;

namespace Football.Domain.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var i in collection)
                action(i);
            return collection;
        }
    }
}
