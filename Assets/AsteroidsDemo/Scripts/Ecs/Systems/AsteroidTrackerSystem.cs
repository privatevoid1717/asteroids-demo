using AsteroidsDemo.Scripts.Ecs.Components;
using AsteroidsDemo.Scripts.Ecs.Systems.Tools;
using AsteroidsDemo.Scripts.Entities.View;
using AsteroidsDemo.Scripts.Physics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Ecs.Systems
{
    public class AsteroidTrackerSystem : IEcsRunSystem
    {
        private EcsCustomInject<AsteroidView> _asteroidPrefabInject;

        private EcsFilterInject<Inc<PositionAndRotationComponent, ViewComponent, RigidBodyComponent, AsteroidComponent>> _filter;

        public void Run(EcsSystems systems)
        {
            var spawnCount = 4 - _filter.Value.GetEntitiesCount();

            if (spawnCount == 0)
            {
                return;
            }

            var world = systems.GetWorld();

            for (int i = 0; i < spawnCount; i++)
            {

                
                var randomPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
                var randomRotation = Random.Range(0, 360);

                var entity = world.NewEntity();
                ref var positionAndRotationComponent = ref _filter.Pools.Inc1.Add(entity);
                positionAndRotationComponent.Position = randomPosition;
                positionAndRotationComponent.Rotation = randomRotation;
                
                var view = Object.Instantiate(_asteroidPrefabInject.Value);
                ref var viewComponent = ref _filter.Pools.Inc2.Add(entity);
                viewComponent.View = view;
                
                ViewSynchronizer.Synchronize(ref positionAndRotationComponent, view);
                
                ref var rbComponent = ref _filter.Pools.Inc3.Add(entity);
                rbComponent.Rigidbody = new CustomRigidbody2();
                ref var asteroidComponent = ref _filter.Pools.Inc4.Add(entity);

                var randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                var randomTorque = Random.Range(-0.1f, 0.1f);
                var speed = Random.Range(1f, 3f);
                rbComponent.Rigidbody.MaxSpeed = speed;
                asteroidComponent.RotationSpeed = randomTorque;
                asteroidComponent.FlyDirection = randomDirection;
            }
        }
    }
}