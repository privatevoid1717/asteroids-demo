using AsteroidsDemo.Scripts.Data;
using AsteroidsDemo.Scripts.Entities.Controller;
using AsteroidsDemo.Scripts.Entities.Controller.Impl;
using AsteroidsDemo.Scripts.Entities.Model;
using AsteroidsDemo.Scripts.Entities.View;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.Model;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Spawn
{
    public class Spawner : MonoBehaviour
    {
        private AsteroidView _asteroidPrefab;
        private PlayerShipView _playerShipPrefab;
        private BulletView _bulletPrefab;
        private LaserView _laserPrefab;
        private AlienView _alienPrefab;

        private IServiceLocator _serviceLocator;

        public Spawner WithServiceLocator(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            return this;
        }

        public Spawner WithPrefabs(PrefabData prefabs)
        {
            _asteroidPrefab = prefabs.AsteroidPrefab;
            _playerShipPrefab = prefabs.PlayerShipPrefab;
            _bulletPrefab = prefabs.BulletPrefab;
            _laserPrefab = prefabs.LaserPrefab;
            _alienPrefab = prefabs.AlienPrefab;
            return this;
        }

        public AsteroidController SpawnAsteroid(Vector3 position)
        {
            var asteroidView = Instantiate(_asteroidPrefab);
            var model = new SimpleModel
            {
                Position = position,
                EulerAngles = new Vector3(0, 0, Random.Range(0f, 360f))
            };

            return new AsteroidController(asteroidView, model, _serviceLocator);
        }

        public PlayerShipController SpawnPlayer(Vector3 position)
        {
            var playerShipView =
                Instantiate(_playerShipPrefab);

            var model = new SpaceShipModel
            {
                Position = position,
                EulerAngles = new Vector3(0, 0, Random.Range(0f, 360f))
            };

            return
                new PlayerShipController(
                    playerShipView,
                    model,
                    _serviceLocator,
                    new LaserController(Instantiate(_laserPrefab), _serviceLocator.GetService<IMessenger>()));
        }

        public AlienController SpawnAlien(Vector3 position, IModel spaceShipModel)
        {
            var alienView = Instantiate(_alienPrefab, position, Quaternion.identity);

            var model = new SimpleModel
            {
                Position = position
            };

            return new AlienController(alienView, model, _serviceLocator, spaceShipModel);
        }

        public BulletController SpawnBullet(Vector3 position, Quaternion rotation)
        {
            var view = Instantiate(_bulletPrefab);

            var model = new SimpleModel
            {
                Position = position,
                EulerAngles = rotation.eulerAngles
            };

            return new BulletController(view, model, _serviceLocator);
        }
    }
}