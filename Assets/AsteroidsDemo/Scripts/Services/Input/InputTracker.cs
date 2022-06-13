using System;
using AsteroidsDemo.Scripts.Interfaces.Services;
using AsteroidsDemo.Scripts.Messages;
using UnityEngine.InputSystem;

namespace AsteroidsDemo.Scripts.Services.Input
{
    public class InputTracker : IInputTracker
    {
        private readonly PlayerInput _playerInput;

        private readonly IMessenger _messenger;

        private InputAction _leftRotateAction;

        private InputAction _fireAction;

        private InputAction _laserAction;

        public event EventHandler Fire;

        public bool RotatingLeft { get; private set; }
        public bool RotatingRight { get; private set; }
        public bool IsMoving { get; private set; }

        public bool IsLaserActive { get; private set; }

        public InputTracker(PlayerInput playerInput, IMessenger messenger)
        {
            _playerInput = playerInput;
            _messenger = messenger;
            Subscribe();
        }

        private void OnNewGame(NewGameMessage obj)
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _playerInput.actions["Fire"].started += OnFire;

            _playerInput.actions["RotateRight"].started += OnRotateRight;
            _playerInput.actions["RotateRight"].canceled += OnRotateRight;

            _playerInput.actions["RotateLeft"].started += OnRotateLeft;
            _playerInput.actions["RotateLeft"].canceled += OnRotateLeft;

            _playerInput.actions["Move"].started += OnMove;
            _playerInput.actions["Move"].canceled += OnMove;

            _playerInput.actions["Laser"].started += OnLaser;
            _playerInput.actions["Laser"].canceled += OnLaser;

            _messenger.Subscribe<NewGameMessage>(OnNewGame);
        }

        private void UnSubscribe()
        {
            _playerInput.actions["Fire"].started -= OnFire;

            _playerInput.actions["RotateRight"].started -= OnRotateRight;
            _playerInput.actions["RotateRight"].canceled -= OnRotateRight;

            _playerInput.actions["RotateLeft"].started -= OnRotateLeft;
            _playerInput.actions["RotateLeft"].canceled -= OnRotateLeft;

            _playerInput.actions["Move"].started -= OnMove;
            _playerInput.actions["Move"].canceled -= OnMove;

            _playerInput.actions["Laser"].started -= OnLaser;
            _playerInput.actions["Laser"].canceled -= OnLaser;

            _messenger.Unsubscribe<NewGameMessage>(OnNewGame);
        }

        private void OnLaser(InputAction.CallbackContext obj)
        {
            IsLaserActive = !obj.canceled;
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            IsMoving = !obj.canceled;
        }

        private void OnRotateLeft(InputAction.CallbackContext obj)
        {
            RotatingLeft = !obj.canceled;
        }

        private void OnRotateRight(InputAction.CallbackContext obj)
        {
            RotatingRight = !obj.canceled;
        }

        private void OnFire(InputAction.CallbackContext obj)
        {
            if (obj.started)
            {
                Fire?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}