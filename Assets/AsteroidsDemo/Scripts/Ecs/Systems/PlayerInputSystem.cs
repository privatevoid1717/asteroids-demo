using AsteroidsDemo.Scripts.Ecs.Components;
using AsteroidsDemo.Scripts.Services.Input;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace AsteroidsDemo.Scripts.Ecs.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private EcsCustomInject<InputTracker> _inputTrackerInject;

        private EcsFilterInject<Inc<InputComponent>> _filter;

        public void Run(EcsSystems systems)
        {
            var tracker = _inputTrackerInject.Value;

            foreach (var e in _filter.Value)
            {
                ref var inputComponent = ref _filter.Pools.Inc1.Get(e);
                inputComponent.RotatingLeft = tracker.RotatingLeft;
                inputComponent.RotatingRight = tracker.RotatingRight;
                inputComponent.IsMoving = tracker.IsMoving;
            }
        }
    }
}