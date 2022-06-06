using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.Model
{
    public class SpaceShipModel
    {
        private float _energy = 10f;
        private float _cooldown;
        public Vector3 Position { get; set; }
        public Vector3 EulerAngles { get; set; }
        public float Speed { get; set; }
        public int Score { get; set; }
        
        public float  MaxEnergy { get; set; }
        public float  MaxCooldown { get; set; }

        public float Energy
        {
            get => _energy;
            set => _energy = Mathf.Clamp(value, 0, MaxEnergy);
        }

        public float Cooldown
        {
            get => _cooldown;
            set => _cooldown = Mathf.Clamp(value, 0, MaxCooldown);
        }
    }
}