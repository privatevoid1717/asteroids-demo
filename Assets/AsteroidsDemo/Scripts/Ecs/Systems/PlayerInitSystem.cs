using AsteroidsDemo.Scripts.Ecs.Components;
using AsteroidsDemo.Scripts.Entities.View;
using AsteroidsDemo.Scripts.Physics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Ecs.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsCustomInject<PlayerShipView> _playerShipPrefabInject;

        private EcsFilterInject<Inc<PositionAndRotationComponent, ViewComponent, InputComponent, RigidBodyComponent>> _filter;

        public void Init(EcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            var view = Object.Instantiate(_playerShipPrefabInject.Value);

            ref var positionComponent = ref _filter.Pools.Inc1.Add(entity);
            ref var viewComponent = ref _filter.Pools.Inc2.Add(entity);
            viewComponent.View = view;
            ref var inputComponent = ref _filter.Pools.Inc3.Add(entity);
            ref var rbComponent = ref _filter.Pools.Inc4.Add(entity);
            var transform = view.transform;
            rbComponent.Rigidbody = new CustomRigidbody2();
        }
    }
}