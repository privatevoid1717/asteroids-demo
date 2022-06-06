using UnityEngine;

namespace AsteroidsDemo.Scripts.Physics
{
    public readonly struct CalculationResult
    {
        public Vector3 Position { get; }
        public Vector3 EulerAngles { get; }
        
        public float Speed { get; }

        public CalculationResult(Vector3 position, float angle, float speed)
        {
            Position = position;
            EulerAngles = new Vector3(0, 0, angle);
            Speed = speed;
        }
    }
}