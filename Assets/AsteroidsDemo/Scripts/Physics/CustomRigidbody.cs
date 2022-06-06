using UnityEngine;

namespace AsteroidsDemo.Scripts.Physics
{
    public class CustomRigidbody
    {
        private float _speed;
        private float _torque;

        private Vector3 _direction;

        private const float Drag = 0.0005f;
        private const float AngularDrag = 0.05f;
        private const float MaxSpeedMultiplier = 0.01f;
        private const float Velocity = 0.002f;

        public float MaxSpeed { get; set; } = 3f;
        public float MaxRotationSpeed { get; set; } = 2f;

        private float Speed
        {
            get => _speed;
            set => _speed = Mathf.Clamp(value, 0, MaxSpeed * MaxSpeedMultiplier);
        }

        private float Torque
        {
            get => _torque;
            set => _torque = Mathf.Clamp(value, -MaxRotationSpeed, MaxRotationSpeed);
        }


        public void AddForce(Vector2 direction)
        {
            _direction = Vector2.Lerp(_direction, direction, 0.1f);
            Speed += direction.magnitude * Velocity;
        }

        public void AddTorque(float force)
        {
            Torque += force;
        }

        public CalculationResult Update(Vector3 currentPosition, float currentAngle)
        {
            if (Mathf.Approximately(Speed, 0f))
            {
                _direction = Vector3.zero;
            }

            Speed = Mathf.Clamp(Speed - Drag, 0f, float.MaxValue);
            Torque = Torque > 0 ? Torque - AngularDrag : Torque + AngularDrag;

            return new CalculationResult(
                currentPosition + _direction.normalized * Speed,
                currentAngle + Torque,
                Speed);
        }
    }
}