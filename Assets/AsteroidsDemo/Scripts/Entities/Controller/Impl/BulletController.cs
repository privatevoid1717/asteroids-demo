﻿using AsteroidsDemo.Scripts.Entities.View;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.Model;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messages;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.Controller.Impl
{
    public class BulletController : ObjectController, IRunnable
    {
        private readonly BulletView _view;
        private readonly IModel _model;
        private float _lifeTime = 0.7f; // TODO в модель

        public void RunInUpdate()
        {
            if (_lifeTime < 0)
            {
                IsAlive = false;
                _view.Destroy();
                return;
            }

            _model.Position += _view.transform.up * (10f * Time.deltaTime);

            _lifeTime -= Time.deltaTime;
        }

        public bool IsAlive { get; private set; } = true;

        public BulletController(BulletView view, IModel model, IServiceLocator serviceLocator) :
            base(view, model, serviceLocator)
        {
            _view = view;
            _view.OnHit += OnHit;

            _model = model;
        }

        private void OnHit(object sender, IObjectView affectedView)
        {
            Messenger.Publish(new HitMessage()
            {
                View = affectedView
            });

            IsAlive = false;
            _view.Destroy();

            _view.OnHit -= OnHit;
        }
    }
}