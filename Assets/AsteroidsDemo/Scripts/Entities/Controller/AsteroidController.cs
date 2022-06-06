using System;
using AsteroidsDemo.Common.Scripts;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messaging;
using AsteroidsDemo.Scripts.Messaging.Messages;
using AsteroidsDemo.Scripts.Physics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidsDemo.Scripts.Entities.Controller
{
    public class AsteroidController : IRunnable
    {
        private readonly IAsteroidView _asteroidView;
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

        public AsteroidController(IAsteroidView asteroidView)
        {
            _asteroidView = asteroidView;
            _randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            _randomTorque = Random.Range(-0.1f, 0.1f);
            _rigidbody.MaxSpeed = Random.Range(0f, 3f);
            _asteroidView.ViewStarted += OnStarted;
        }

        private void OnStarted(object sender, EventArgs e)
        {
            SimpleMessenger.Subscribe<HitMessage>(OnBulletHit, m => m.View == _asteroidView);
        }

        private void OnBulletHit(HitMessage obj)
        {
            IsAlive = false;

            SimpleMessenger.Publish(new AsteroidDestroyedMessage()
            {
                Position = _asteroidView.Position,
                IsDebris = IsDebris
            });

            _asteroidView.Destroy();
            
            SimpleMessenger.Unsubscribe<HitMessage>(OnBulletHit);
        }

        public void RunFixedUpdate()
        {
            _rigidbody.AddForce(_randomDirection);

            _rigidbody.AddTorque(_randomTorque);

            var calculationResult =
                _rigidbody.Update(_asteroidView.Position, _asteroidView.EulerAngles.z);

            _asteroidView.SetPosition(calculationResult.Position);
            _asteroidView.SetRotation(calculationResult.EulerAngles);
        }

        public void RunInUpdate()
        {
        }


        public bool IsAlive { get; private set; } = true;
    }
}