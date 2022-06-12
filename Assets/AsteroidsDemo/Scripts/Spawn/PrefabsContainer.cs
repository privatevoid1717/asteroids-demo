using AsteroidsDemo.Scripts.Entities.View;

namespace AsteroidsDemo.Scripts.Spawn
{
    public class PrefabsContainer
    {
        public AsteroidView AsteroidPrefab { get; set; }
        public PlayerShipView PlayerShipPrefab { get; set; }
        public BulletView BulletPrefab { get; set; }
        public LaserView LaserPrefab { get; set; }
        public AlienView AlienPrefab { get; set; }
    }
}