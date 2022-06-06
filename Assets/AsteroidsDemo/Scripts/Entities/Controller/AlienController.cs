using System;
using AsteroidsDemo.Common.Scripts;
using AsteroidsDemo.Scripts.Entities.Model;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messaging;
using AsteroidsDemo.Scripts.Messaging.Messages;
using AsteroidsDemo.Scripts.Physics;

namespace AsteroidsDemo.Scripts.Entities.Controller
{
    public class AlienController : IRunnable
    {
        private readonly IAlienView _alienView;
        private readonly CustomRigidbody _rigidbody = new();
        private readonly SpaceShipModel _playerModel;

        public AlienController(IAlienView alienView, SpaceShipModel playerModel)
        {
            _playerModel = playerModel;

            _alienView = alienView;
            _rigidbody.MaxSpeed = 2f;
            _alienView.ViewStarted += OnStarted;
        }

        private void OnStarted(object sender, EventArgs e)
        {
            SimpleMessenger.Subscribe<HitMessage>(OnHit, m => m.View == _alienView);
        }


        private void OnHit(HitMessage obj)
        {
            IsAlive = false;
            
            SimpleMessenger.Publish(new AlienDestroyedMessage()
            {
                Position = _alienView.Position,
            });

            _alienView.Destroy();

            SimpleMessenger.Unsubscribe<HitMessage>(OnHit);
        }

        public void RunFixedUpdate()
        {
            var dir = _playerModel.Position - _alienView.Position; // TODO учитывать портал и выбирать ближайший путь

            _rigidbody.AddForce(dir.normalized);

            var calculationResult =
                _rigidbody.Update(_alienView.Position, _alienView.Position.z);

            _alienView.SetPosition(calculationResult.Position);
            _alienView.SetRotation(calculationResult.EulerAngles);
        }

        public void RunInUpdate()
        {
        }


        public bool IsAlive { get; private set; } = true;
    }
}