using AsteroidsDemo.Common.Scripts;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messaging;
using AsteroidsDemo.Scripts.Messaging.Messages;
using AsteroidsDemo.SpaceShip.Scripts.View;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.Controller
{
    public class BulletController : IRunnable
    {
        private readonly BulletView _view;

        private float _lifeTime = 0.7f;

        public void RunInUpdate()
        {
            if (_lifeTime < 0)
            {
                IsAlive = false;
                _view.Destroy();
                return;
            }

            var transform = _view.transform;
            transform.position += transform.up * (10f * Time.deltaTime);

            _lifeTime -= Time.deltaTime;
        }

        public bool IsAlive { get; private set; } = true;

        public BulletController(BulletView view)
        {
            _view = view;
            _view.OnHit += OnHit;
        }

        private void OnHit(object sender, IObjectView affectedView)
        {
            SimpleMessenger.Publish(new HitMessage()
            {
                View = affectedView
            });

            IsAlive = false;
            _view.Destroy();

            _view.OnHit -= OnHit;
        }


        public void RunFixedUpdate()
        {
        }
    }
}