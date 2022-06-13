using System;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.Model;
using AsteroidsDemo.Scripts.Interfaces.Services;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messages;
using AsteroidsDemo.Scripts.Physics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidsDemo.Scripts.Entities.Controller.Impl
{
    public class AsteroidController : ObjectController, IRunnable
    {
        private readonly IAsteroidView _asteroidView;
        private readonly IModel _model;
        private readonly CustomRigidbody _rigidbody = new();
        private Vector3 _randomDirection;
        private readonly float _randomTorque;
        private bool _isDebris;

        public bool IsDebris
        {
            get => _isDebris;
            set
            {
                _isDebris = value;
                if (_isDebris)
                {
                    _asteroidView.SetScale(0.2f);
                    _rigidbody.MaxSpeed = 5f;
                    _randomDirection =
                        new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * 20f;
                }
            }
        }

        public AsteroidController(IAsteroidView asteroidView, IModel model, IServiceLocator serviceLocator)
            : base(asteroidView, model, serviceLocator)
        {
            _asteroidView = asteroidView;
            _model = model;
            _randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            _randomTorque = Random.Range(-0.1f, 0.1f);
            _rigidbody.MaxSpeed = Random.Range(0f, 3f);
            _asteroidView.ViewStarted += OnStarted;
        }

        private void OnStarted(object sender, EventArgs e)
        {
            Messenger.Subscribe<HitMessage>(OnBulletHit, m => m.View == _asteroidView);
        }

        private void OnBulletHit(HitMessage obj)
        {
            IsAlive = false;

            Messenger.Publish(new AsteroidDestroyedMessage()
            {
                Position = _asteroidView.Position,
                IsDebris = IsDebris
            });

            _asteroidView.Destroy();

            Messenger.Unsubscribe<HitMessage>(OnBulletHit);
        }

        public override void RunFixedUpdate()
        {
            _rigidbody.AddForce(_randomDirection);

            _rigidbody.AddTorque(_randomTorque);

            var calculationResult =
                _rigidbody.Update(_asteroidView.Position, _asteroidView.EulerAngles.z);

            _model.Position = calculationResult.Position;
            _model.EulerAngles = calculationResult.EulerAngles;

            base.RunFixedUpdate();
        }

        public void RunInUpdate()
        {
        }


        public bool IsAlive { get; private set; } = true;
    }
}