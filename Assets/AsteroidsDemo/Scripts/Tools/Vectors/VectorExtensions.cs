using UnityEngine;

namespace AsteroidsDemo.Scripts.Tools.Vectors
{
    public static class VectorExtensions
    {
        public static Vector2 XY(this Vector3 vector) => new Vector2(vector.x, vector.y);

        public static Vector3 WithX(this Vector3 vector, float x) => new Vector3(x, vector.y, vector.z);

        public static Vector3 WithY(this Vector3 vector, float y) => new Vector3(vector.x, y, vector.z);

        public static Vector3 WithZ(this Vector3 vector, float z) => new Vector3(vector.x, vector.y, z);

        public static Vector3 AddZ(this Vector2 vector, float z) => new Vector3(vector.x, vector.y, z);


        /// <summary>
        /// Случайная позиция в заданом радиусе
        /// </summary>
        /// <param name="original"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector2 RandomPosition(this Vector2 original, float radius) =>
            original + UnityEngine.Random.insideUnitCircle * radius;

        /// <summary>
        /// Случайная позиция в заданом радиусе, игнорируя Z
        /// </summary>
        /// <param name="original"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector3 RandomPositionWithinRadius(this Vector3 original, float radius)
        {
            var z = original.z;
            var randomVector2 = (Vector2) original + UnityEngine.Random.insideUnitCircle * radius;
            return new Vector3(randomVector2.x, randomVector2.y, z);
        }

        /// <summary>
        /// Рандом от -range до +range
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public static Vector2 Random(float range) =>
            Random(range, range);

        public static Vector2 Random(float rangeX, float rangeY) =>
            new Vector2(
                UnityEngine.Random.Range(-rangeX, rangeX),
                UnityEngine.Random.Range(-rangeY, rangeY));

        public static bool IsInRange(this Vector2 position, float x, float y)
        {
            return position.x < x &&
                   position.x > -x &&
                   position.y < -y &&
                   position.y > y;
        }

        public static float DistanceTo(this Vector3 vector, Vector3 target) => Vector2.Distance(vector, target);
        public static float DistanceTo(this Vector2 vector, Vector2 target) => Vector2.Distance(vector, target);
        public static Vector2 DirectionTo(this Vector2 vector2, Vector2 target) => target - vector2;
        
        public static float AngleTo(this Vector2 vector, Vector2 target)
        {
            var targetDir = vector.DirectionTo(target);
            return Vector2.SignedAngle(targetDir, vector);
        }
    }
}