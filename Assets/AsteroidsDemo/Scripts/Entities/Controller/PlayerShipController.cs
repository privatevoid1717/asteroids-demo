using System;
using System.Timers;
using AsteroidsDemo.Scripts.Entities.Model;
using AsteroidsDemo.Scripts.Input;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messaging;
using AsteroidsDemo.Scripts.Messaging.Messages;
using AsteroidsDemo.Scripts.Physics;
using UnityEngine;
using Zenject;

namespace AsteroidsDemo.Scripts.Entities.Controller
{
    public class PlayerShipController : IRunnable
    {
        private readonly IPlayerShipView _playerShipView;
        private readonly CustomRigidbody _rigidbody = new();
        private readonly LaserController _laser;
        private readonly Timer _timer = new();
        private readonly InputStatus _inputState;

        private const float MaxEnergy = 10f;
        private const float MaxEnergyCooldown = 10f;

        public SpaceShipModel Model { get; } = new();

        [Inject]
        public PlayerShipController(IPlayerShipView playerShipView, LaserController laser, InputStatus state)
        {
            _inputState = state;
            _playerShipView = playerShipView;

            Model.Position = _playerShipView.Position;
            Model.Energy = MaxEnergy;
            Model.MaxEnergy = MaxEnergy;
            Model.MaxCooldown = MaxEnergyCooldown;
            _laser = laser;

            SimpleMessenger.Subscribe<DestroyedMessage>(OnTargetDestroyed);

            _playerShipView.PlayerWasHit += OnPlayerWasHit;
            _inputState.Fire += OnFire;

            _timer.Interval = 100f;
            _timer.Start();
            _timer.Elapsed += Elapsed;
        }

        private void OnFire(object sender, EventArgs e)
        {
            SimpleMessenger.Publish(new FireMessage()
            {
                Position = _playerShipView.Position, EulerAngles = _playerShipView.EulerAngles
            });
        }

        private void OnPlayerWasHit(object sender, EventArgs e)
        {
            _timer.Elapsed -= Elapsed;
            _playerShipView.PlayerWasHit -= OnPlayerWasHit;
            SimpleMessenger.Publish(new PlayerDestroyedMessage());
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


        public void RunFixedUpdate()
        {
            if (_inputState.RotatingLeft)
            {
                _rigidbody.AddTorque(0.1f);
            }

            if (_inputState.RotatingRight)
            {
                _rigidbody.AddTorque(-0.1f);
            }

            if (_inputState.IsMoving)
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

            SyncView();
        }

        private void SyncView()
        {
            _playerShipView.SetPosition(Model.Position);
            _playerShipView.SetRotation(Model.EulerAngles);
        }

        public void RunInUpdate()
        {
            if (_inputState.IsLaserActive && Model.Energy >= 0.1f)
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