using AsteroidsDemo.Common.Scripts;
using AsteroidsDemo.Entities.Weapon.Laser;
using AsteroidsDemo.Infrastructure.Scripts.PortableObject;
using AsteroidsDemo.Scripts.Entities.Controller;
using AsteroidsDemo.Scripts.Entities.Model;
using AsteroidsDemo.SpaceShip.Scripts;
using AsteroidsDemo.SpaceShip.Scripts.View;
using UnityEngine;
using Zenject;

namespace AsteroidsDemo.Scripts.Spawner
{
    public class Spawner : MonoBehaviour
    {
        [field: SerializeField] private PortableObjectView asteroidPrefab;
        [field: SerializeField] private PlayerShipView playerShipPrefab;
        [field: SerializeField] private BulletView bulletPrefab;
        [field: SerializeField] private LaserView laserPrefab;
        [field: SerializeField] private AlienView alienPrefab;

        [Inject] private DiContainer _container;

        public AsteroidController SpawnAsteroid(Vector3 position)
        {
            var asteroidView = Instantiate(asteroidPrefab, position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            return _container.Instantiate<AsteroidController>(new[] {asteroidView});
        }

        public PlayerShipController SpawnPlayer(Vector3 position)
        {
            var playerShipView =
                Instantiate(playerShipPrefab, position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));

            return _container.Instantiate<PlayerShipController>(new object[]
            {
                playerShipView,
                new LaserController(Instantiate(laserPrefab))   
            });
        }

        public AlienController SpawnAlien(Vector3 position, SpaceShipModel spaceShipModel)
        {
            var alienView = Instantiate(alienPrefab, position, Quaternion.identity);

            return _container.Instantiate<AlienController>(new object[] {alienView, spaceShipModel});
        }

        public BulletController SpawnBullet(Vector3 position, Quaternion rotation)
        {
            return new BulletController(Instantiate(bulletPrefab, position, rotation));
        }
    }
}