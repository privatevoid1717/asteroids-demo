using System;
using System.Collections.Generic;
using System.Linq;

namespace AsteroidsDemo.Scripts.Tools.Collections
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(
            this IEnumerable<T> source,
            Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        public static void ForEach<T>(
            this IEnumerable<T> source,
            Action<T, int> action)
        {
            var enumerable = source as T[] ?? source.ToArray();
            for (var i = 0; i < enumerable.Count(); i++)
            {
                action(enumerable.ElementAt(i), i);
            }
        }
    }
}