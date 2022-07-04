using System;
using AsteroidsDemo.Scripts.CameraManagement;
using AsteroidsDemo.Scripts.Ecs.Data;
using AsteroidsDemo.Scripts.Ecs.Systems;
using AsteroidsDemo.Scripts.Services.Input;
using AsteroidsDemo.Scripts.Services.Messaging;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Ecs.Startup
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameData gameStaticData;

        private EcsSystems _fixedUpdateSystem;

        private void Start()
        {
            var world = new EcsWorld();
            _fixedUpdateSystem = new EcsSystems(world);

            var inputTracker = new InputTracker(Instantiate(gameStaticData.PlayerInput), new SimpleMessenger());

            _fixedUpdateSystem
                .Add(new PlayerInitSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerInputSystem())
                .Add(new AsteroidTrackerSystem())
                .Add(new SimpleMovementSystem())
                .Add(new PositionResolverSystem())
                .Inject(gameStaticData.PlayerShipViewPrefab)
                .Inject(gameStaticData.AsteroidViewPrefab)
                .Inject(inputTracker)
                .Inject(FindObjectOfType<TiledCamera>())
                .Init();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystem.Run();
        }
    }
}