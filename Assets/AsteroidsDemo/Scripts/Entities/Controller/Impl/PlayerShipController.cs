using System;
using System.Timers;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.Model;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messages;
using AsteroidsDemo.Scripts.Physics;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.Controller.Impl
{
    public class PlayerShipController : ObjectController, IRunnable
    {
        private readonly IPlayerShipView _playerShipView;
        private readonly CustomRigidbody _rigidbody = new();
        private readonly LaserController _laser;
        private readonly Timer _timer = new();
        private readonly IInputTracker _inputTracker;

        private const float MaxEnergy = 10f;
        private const float MaxEnergyCooldown = 10f;

        public ISpaceShipModel Model { get; }

        public PlayerShipController(
            IPlayerShipView playerShipView,
            ISpaceShipModel model,
            IServiceLocator serviceLocator,
            LaserController laser
        ) : base(playerShipView, model, serviceLocator)
        {
            _inputTracker = serviceLocator.GetService<IInputTracker>();

            _playerShipView = playerShipView;

            Model = model;
            Model.Position = _playerShipView.Position;
            Model.Energy = MaxEnergy;
            Model.MaxEnergy = MaxEnergy;
            Model.MaxCooldown = MaxEnergyCooldown;
            _laser = laser;

            Messenger.Subscribe<DestroyedMessage>(OnTargetDestroyed);

            _playerShipView.PlayerWasHit += OnPlayerWasHit;
            _inputTracker.Fire += OnFire;

            _timer.Interval = 100f;
            _timer.Start();
            _timer.Elapsed += Elapsed;
        }

        private void OnFire(object sender, EventArgs e)
        {
            Messenger.Publish(new FireMessage()
            {
                Position = _playerShipView.Position, EulerAngles = _playerShipView.EulerAngles
            });
        }

        private void OnPlayerWasHit(object sender, EventArgs e)
        {
            _timer.Elapsed -= Elapsed;
            _playerShipView.PlayerWasHit -= OnPlayerWasHit;
            Messenger.Publish(new PlayerDestroyedMessage());
        }

        private void OnTargetDestroyed(DestroyedMessage obj)
        {
            Model.Score += 1;
        }

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Model.Cooldown <= 0 && Model.Energy < MaxEnergy)
            {
                Model.Energy += 1f;
                Model.Cooldown = MaxEnergyCooldown;
            }

            Model.Cooldown -= e.SignalTime.Millisecond * 0.001f;
        }


        public override void RunFixedUpdate()
        {
            if (_inputTracker.RotatingLeft)
            {
                _rigidbody.AddTorque(0.1f);
            }

            if (_inputTracker.RotatingRight)
            {
                _rigidbody.AddTorque(-0.1f);
            }

            if (_inputTracker.IsMoving)
            {
                _rigidbody.AddForce(_playerShipView.Direction);
                _playerShipView.SetForsage(true);
            }
            else
            {
                _playerShipView.SetForsage(false);
            }

            var calculationResult =
                _rigidbody.Update(_playerShipView.Position, _playerShipView.EulerAngles.z);

            Model.Position = calculationResult.Position;
            Model.EulerAngles = calculationResult.EulerAngles;
            Model.Speed = calculationResult.Speed;

            base.RunFixedUpdate();
        }

        public void RunInUpdate()
        {
            if (_inputTracker.IsLaserActive && Model.Energy >= 0.1f)
            {
                _laser.Fire(_playerShipView.Position, _playerShipView.Direction);
                Model.Energy -= Time.deltaTime * 3f;
            }
            else
            {
                _laser.Hold();
            }
        }

        public bool IsAlive => true;
    }
}