using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.Model;
using AsteroidsDemo.Scripts.Interfaces.Services;
using AsteroidsDemo.Scripts.Interfaces.View;

namespace AsteroidsDemo.Scripts.Entities.Controller
{
    public abstract class ObjectController
    {
        private readonly IModel _model;
        private readonly IObjectView _view;
        private readonly IPortableObjectPositionResolver _positionResolver;

        protected ObjectController(IObjectView view, IModel model, IServiceLocator serviceLocator)
        {
            _positionResolver = serviceLocator.GetService<IPortableObjectPositionResolver>();
            _view = view;
            _model = model;
            
            SyncView();
        }

        private void SyncView()
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