using System;
using AsteroidsDemo.Common.Scripts;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.View;
using UnityEngine;
using Zenject;

namespace AsteroidsDemo.Infrastructure.Scripts.PortableObject
{
    public class PortableObjectView : MonoBehaviour, IObjectView
    {
        [Inject] private ITiledCamera _camera;

        public Vector3 Position => transform.position;
        public Vector3 Direction => transform.up;
        public Vector3 EulerAngles => transform.eulerAngles;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Vector3 eulerAngles)
        {
            transform.eulerAngles = eulerAngles;
        }

        public event EventHandler ViewStarted;

        protected void Start()
        {
            ViewStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void FixedUpdate()
        {
            var position = transform.position;
            var screenPos = _camera.MainCamera.WorldToScreenPoint(position);

            var newY =
                screenPos.y < 0 ? screenPos.y + _camera.Height :
                screenPos.y > _camera.Height ? screenPos.y - _camera.Height : screenPos.y;
            var newX =
                screenPos.x < 0 ? screenPos.x + _camera.Width :
                screenPos.x > _camera.Width ? screenPos.x - _camera.Width : screenPos.x;

            var newScreenPosition = _camera.MainCamera.ScreenToWorldPoint(new Vector2(newX, newY));

            var newPosition = new Vector3(newScreenPosition.x, newScreenPosition.y, position.z);
            transform.position = newPosition;
        }
    }
}