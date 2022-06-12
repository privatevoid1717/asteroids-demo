using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Tools.Vectors;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Services.PositionResolver
{
    public class PortableObjectPositionResolver : IPortableObjectPositionResolver
    {
        private readonly ITiledCamera _tiledCamera;

        public PortableObjectPositionResolver(ITiledCamera tiledCamera)
        {
            _tiledCamera = tiledCamera;
        }

        public Vector3 Resolve(Vector3 position)
        {
            var screenPos = _tiledCamera.MainCamera.WorldToScreenPoint(position);

            var newY =
                screenPos.y < 0 ? screenPos.y + _tiledCamera.Height :
                screenPos.y > _tiledCamera.Height ? screenPos.y - _tiledCamera.Height : screenPos.y;
            var newX =
                screenPos.x < 0 ? screenPos.x + _tiledCamera.Width :
                screenPos.x > _tiledCamera.Width ? screenPos.x - _tiledCamera.Width : screenPos.x;

            return _tiledCamera.MainCamera.ScreenToWorldPoint(new Vector2(newX, newY)).WithZ(position.z);
        }
    }
}