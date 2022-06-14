using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.Model;
using AsteroidsDemo.Scripts.Interfaces.Services;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messages;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.Controller.Impl
{
    public class BulletController : ObjectController, IRunnable
    {
        private readonly IBulletView _view;
        private readonly IModel _model;
        private readonly IMessenger _messenger;
        private float _lifeTime = 0.7f; // TODO в модель

        public void RunUpdate()
        {
            if (_lifeTime < 0)
            {
                IsAlive = false;
                _view.Destroy();
                return;
            }

            _model.Position += _view.Direction * (10f * Time.deltaTime);

            _lifeTime -= Time.deltaTime;
        }

        public bool IsAlive { get; private set; } = true;

        public BulletController(IBulletView view, IModel model, IServiceLocator serviceLocator) :
            base(view, model, serviceLocator)
        {
            _messenger = serviceLocator.GetService<IMessenger>();
            
            _view = view;
            _view.OnHit += OnHit;

            _model = model;
        }

        private void OnHit(object sender, IObjectView affectedView)
        {
            _messenger.Publish(new HitMessage()
            {
                View = affectedView
            });

            IsAlive = false;
            _view.Destroy();

            _view.OnHit -= OnHit;
        }
    }
}