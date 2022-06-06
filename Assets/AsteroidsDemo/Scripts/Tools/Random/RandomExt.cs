using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Tools.Random
{
    public static class RandomExt
    {
        private static readonly int[] Signs = {-1, 1};

        public static int RandomSign()
        {
            return Signs[UnityEngine.Random.Range(0, 2)];
        }

        public static T GetRandomOrDefault<T>(this IEnumerable<T> collection)
        {
            var enumerable = collection as T[] ?? collection.ToArray();

            return !enumerable.Any()
                ? default(T)
                : enumerable.ElementAt(UnityEngine.Random.Range(0, enumerable.Count()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="successChance">от 0 до 1</param>
        /// <returns></returns>
        public static bool RandomRoll(float successChance)
        {
            return UnityEngine.Random.Range(0f, 1f) <= successChance;
        }
        
        public static Vector2 RandomVector2(float minX, float maxX, float minY, float maxY) =>
            new Vector2(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY));
        
        
        
    }
}