using AsteroidsDemo.Scripts.Ecs.Components;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Tools.Vectors;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Ecs.Systems
{
    public class PositionResolverSystem : IEcsRunSystem
    {
        private EcsCustomInject<ITiledCamera> _tiledCameraInject;

        private EcsFilterInject<Inc<PositionAndRotationComponent>> _filter;

        public void Run(EcsSystems systems)
        {
            var tiledCamera = _tiledCameraInject.Value;

            foreach (var e in _filter.Value)
            {
                ref var position = ref _filter.Pools.Inc1.Get(e);

                var screenPos = tiledCamera.MainCamera.WorldToScreenPoint(position.Position);

                var newY =
                    screenPos.y < 0 ? screenPos.y + tiledCamera.Height :
                    screenPos.y > tiledCamera.Height ? screenPos.y - tiledCamera.Height : screenPos.y;
                var newX =
                    screenPos.x < 0 ? screenPos.x + tiledCamera.Width :
                    screenPos.x > tiledCamera.Width ? screenPos.x - tiledCamera.Width : screenPos.x;

                position.Position = tiledCamera.MainCamera.ScreenToWorldPoint(new Vector2(newX, newY));
            }
        }
    }
}