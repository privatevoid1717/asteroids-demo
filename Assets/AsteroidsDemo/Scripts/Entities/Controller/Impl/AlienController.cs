using System;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.Model;
using AsteroidsDemo.Scripts.Interfaces.Services;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messages;
using AsteroidsDemo.Scripts.Physics;

namespace AsteroidsDemo.Scripts.Entities.Controller.Impl
{
    public class AlienController : ObjectController, IRunnable
    {
        private readonly IAlienView _alienView;
        private readonly CustomRigidbody _rigidbody = new();
        private readonly IModel _playerModel;
        private readonly IModel _model;
        private readonly IMessenger _messenger;

        public AlienController(
            IAlienView alienView,
            IModel model,
            IServiceLocator serviceLocator,
            IModel playerModel) :
            base(alienView, model, serviceLocator)
        {
            _model = model;
            _playerModel = playerModel;
            _alienView = alienView;
            _rigidbody.MaxSpeed = 2f;
            _messenger = serviceLocator.GetService<IMessenger>();
            _alienView.ViewStarted += OnStarted;
        }

        private void OnStarted(object sender, EventArgs e)
        {
            _messenger.Subscribe<HitMessage>(OnHit, m => m.View == _alienView);
        }


        private void OnHit(HitMessage obj)
        {
            IsAlive = false;

            _messenger.Publish(new AlienDestroyedMessage()
            {
                Position = _alienView.Position,
            });

            _alienView.Destroy();

            _messenger.Unsubscribe<HitMessage>(OnHit);
        }

        public override void RunFixedUpdate()
        {
            var dir = _playerModel.Position - _alienView.Position; // TODO учитывать портал и выбирать ближайший путь

            _rigidbody.AddForce(dir.normalized);

            var calculationResult =
                _rigidbody.Update(_alienView.Position, _alienView.Position.z);

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