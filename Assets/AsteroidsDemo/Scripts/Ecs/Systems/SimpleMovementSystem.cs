using AsteroidsDemo.Scripts.Ecs.Components;
using AsteroidsDemo.Scripts.Ecs.Systems.Tools;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace AsteroidsDemo.Scripts.Ecs.Systems
{
    public class SimpleMovementSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PositionAndRotationComponent, RigidBodyComponent, ViewComponent, AsteroidComponent>> _filter;

        public void Run(EcsSystems systems)
        {
            foreach (var e in _filter.Value)
            {
                ref var positionAndRotationComponent = ref _filter.Pools.Inc1.Get(e);
                ref var rb = ref _filter.Pools.Inc2.Get(e);
                ref var viewComponent = ref _filter.Pools.Inc3.Get(e);
                ref var asteroidComponent = ref _filter.Pools.Inc4.Get(e);

                rb.Rigidbody.AddTorque(asteroidComponent.RotationSpeed);
                rb.Rigidbody.AddForce(asteroidComponent.FlyDirection);

                PositionAndRotationSynchronizer.UpdateAndApply(ref positionAndRotationComponent, rb.Rigidbody);
                ViewSynchronizer.Synchronize(ref positionAndRotationComponent, viewComponent.View);
            }
        }
    }
}