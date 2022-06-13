using System.Collections.Generic;
using System.Linq;
using AsteroidsDemo.Scripts.Data;
using AsteroidsDemo.Scripts.Effects;
using AsteroidsDemo.Scripts.Entities.Controller.Impl;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.Services;
using AsteroidsDemo.Scripts.Messages;
using AsteroidsDemo.Scripts.ServiceResolving;
using AsteroidsDemo.Scripts.Services.Input;
using AsteroidsDemo.Scripts.Services.Messaging;
using AsteroidsDemo.Scripts.Services.PositionResolver;
using AsteroidsDemo.Scripts.Spawn;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace AsteroidsDemo.Scripts.Startup
{
    public class Game : MonoBehaviour
    {
        private Spawner _spawner;

        [SerializeField] private int minAsteroids = 3;

        [SerializeField] private PrefabData prefabData;

        private readonly List<IRunnable> _runnables = new();

        private IServiceLocator _serviceLocator;

        private void Awake()
        {
            // TODO расчитывать границы исходя из соотношения сторон (сейчас работает корректно только на 16:9)
            Screen.SetResolution(1920, 1080, false);
            
            InitializeServices();

            var messenger = _serviceLocator.GetService<IMessenger>();

            _spawner =
                Instantiate(prefabData.SpawnerPrefab)
                    .GetComponent<Spawner>()
                    .WithServiceLocator(_serviceLocator)
                    .WithPrefabs(prefabData);

            Instantiate(prefabData.VfxPlayer.GetComponent<VfxPlayer>())
                .WithMessenger(messenger)
                .WithPrefabs(prefabData);

            var canvas = FindObjectOfType<Canvas>();
            Instantiate(prefabData.Hud, canvas.transform).WithMessenger(messenger);
            Instantiate(prefabData.GameOver, canvas.transform).WithMessenger(messenger);
        }


        private void InitializeServices()
        {
            var messenger = new SimpleMessenger();
            var inputTracker = new InputTracker(Instantiate(prefabData.PlayerInputPrefab), messenger);

            _serviceLocator = new ServiceLocator()
                .WithService(messenger)
                .WithService(inputTracker)
                .WithService(new PortableObjectPositionResolver(Camera.main.GetComponent<ITiledCamera>()));
        }


        private void OnEnable()
        {
            var messenger = _serviceLocator.GetService<IMessenger>();

            messenger.Subscribe<FireMessage>(OnFire);
            messenger.Subscribe<AsteroidDestroyedMessage>(OnAsteroidDestroyed);
            messenger.Subscribe<AlienDestroyedMessage>(OnAlienDestroyedDestroyed);
            messenger.Subscribe<NewGameMessage>(OnNewGame);
        }

        private void OnNewGame(NewGameMessage obj)
        {
            _serviceLocator.GetService<IMessenger>().UnsubscribeAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnAlienDestroyedDestroyed(AlienDestroyedMessage obj)
        {
            _runnables.Add(
                _spawner.SpawnAlien(
                    new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0),
                    ((PlayerShipController) _runnables.Single(r => r is PlayerShipController)).Model));
        }

        private void OnAsteroidDestroyed(AsteroidDestroyedMessage asteroidDestroyedMessage)
        {
            if (!asteroidDestroyedMessage.IsDebris)
            {
                for (int i = 0; i < 3; i++)
                {
                    var debris = _spawner.SpawnAsteroid(asteroidDestroyedMessage.Position);
                    debris.IsDebris = true;
                    _runnables.Add(debris);
                }
            }

            if (_runnables.Where(r => r is AsteroidController).Count(c => c.IsAlive) < minAsteroids)
            {
                _runnables.Add(
                    _spawner.SpawnAsteroid(new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0)));
            }
        }


        private void OnFire(FireMessage fireMessage)
        {
            var bullet = _spawner.SpawnBullet(fireMessage.Position, Quaternion.Euler(fireMessage.EulerAngles));
            _runnables.Add(bullet);
        }

        private void Start()
        {
            var player = _spawner.SpawnPlayer(Vector3.zero);
            _serviceLocator.GetService<IMessenger>().Publish(new PlayerSpawnedMessage()
            {
                PlayerModel = player.Model
            });

            _runnables.Add(player);

            for (var i = 0; i < 3; i++)
            {
                _runnables.Add(
                    _spawner.SpawnAsteroid(new Vector3(Random.Range(-100, 100), Random.Range(-100, 100),
                        0))); // TODO не спавнить на игрока
            }

            _runnables.Add(
                _spawner.SpawnAlien(
                    new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), 0),
                    player.Model)); // TODO не спавнить на игрока
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _runnables.Count; i++)
            {
                var runnable = _runnables[i];
                if (!runnable.IsAlive)
                {
                    _runnables.RemoveAt(i);
                    i--;
                }
                else
                {
                    runnable.RunFixedUpdate();
                }
            }
        }

        private void Update()
        {
            for (int i = 0; i < _runnables.Count; i++)
            {
                var runnable = _runnables[i];
                if (!runnable.IsAlive)
                {
                    _runnables.RemoveAt(i);
                    i--;
                }
                else
                {
                    runnable.RunInUpdate();
                }
            }
        }
    }
}