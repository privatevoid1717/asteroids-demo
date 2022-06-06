using UnityEngine;

namespace AsteroidsDemo.Scripts.Tools.Primitives
{
    public static class FloatExtensions
    {
        public static int Sign(this float val) => val < 0 ? -1 : 1;

        public static float Abs(this float value) => Mathf.Abs(value);
    }
}