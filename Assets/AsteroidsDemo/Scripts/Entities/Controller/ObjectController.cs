using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.Model;
using AsteroidsDemo.Scripts.Interfaces.View;

namespace AsteroidsDemo.Scripts.Entities.Controller
{
    public abstract class ObjectController
    {
        protected IMessenger Messenger { get; private set; }
        private readonly IModel _model;
        private readonly IObjectView _view;
        private readonly IPortableObjectPositionResolver _positionResolver;

        protected ObjectController(IObjectView view, IModel model, IServiceLocator serviceLocator)
        {
            Messenger = serviceLocator.GetService<IMessenger>();
            _positionResolver = serviceLocator.GetService<IPortableObjectPositionResolver>();
            _view = view;
            _model = model;
            
            SyncView();
        }

        protected void SyncView()
        {
            _view.SetPosition(_model.Position);
            _view.SetRotation(_model.EulerAngles);
        }

        public virtual void RunFixedUpdate()
        {
            _model.Position = _positionResolver.Resolve(_model.Position);
            SyncView();
        }
    }
}