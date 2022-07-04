using AsteroidsDemo.Scripts.Ecs.Components;
using AsteroidsDemo.Scripts.Ecs.Systems.Tools;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace AsteroidsDemo.Scripts.Ecs.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PositionAndRotationComponent, RigidBodyComponent, ViewComponent, InputComponent>> _filter;

        public void Run(EcsSystems systems)
        {
            foreach (var e in _filter.Value)
            {
                ref var positionComponent = ref _filter.Pools.Inc1.Get(e);
                ref var rb = ref _filter.Pools.Inc2.Get(e);
                ref var viewComponent = ref _filter.Pools.Inc3.Get(e);
                ref var input = ref _filter.Pools.Inc4.Get(e);

                if (input.RotatingLeft)
                {
                    rb.Rigidbody.AddTorque(0.1f);
                }

                if (input.RotatingRight)
                {
                    rb.Rigidbody.AddTorque(-0.1f);
                }

                if (input.IsMoving)
                {
                    rb.Rigidbody.AddForce(viewComponent.View.Direction);
                }

                PositionAndRotationSynchronizer.UpdateAndApply(ref positionComponent, rb.Rigidbody);
                ViewSynchronizer.Synchronize(ref positionComponent, viewComponent.View);
            }
        }
    }
}