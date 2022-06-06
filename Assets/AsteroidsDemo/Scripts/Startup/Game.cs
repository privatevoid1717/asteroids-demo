using System.Collections.Generic;
using System.Linq;
using AsteroidsDemo.Scripts.Entities.Controller;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Messaging;
using AsteroidsDemo.Scripts.Messaging.Messages;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

namespace AsteroidsDemo.Scripts.Startup
{
    public class Game : MonoBehaviour
    {
        [Inject] private Spawner.Spawner _spawner;

        [SerializeField] private int minAsteroids = 3;

        private readonly IList<IRunnable> _runnables = new List<IRunnable>();

        private void Awake()
        {
            Screen.SetResolution(1920, 1080, false);
            // TODO расчитывать границы исходя из соотношения сторон (сейчас работает корректно только на 16:9)
        }


        private void OnEnable()
        {
            SimpleMessenger.Subscribe<FireMessage>(OnFire);
            SimpleMessenger.Subscribe<AsteroidDestroyedMessage>(OnAsteroidDestroyed);
            SimpleMessenger.Subscribe<AlienDestroyedMessage>(OnAlienDestroyedDestroyed);
            SimpleMessenger.Subscribe<NewGameMessage>(OnNewGame);
        }

        private void OnNewGame(NewGameMessage obj)
        {
            SimpleMessenger.UnsubscribeAll();
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
            SimpleMessenger.Publish(new PlayerSpawnedMessage()
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