using System;

namespace AsteroidsDemo.Scripts.Interfaces.View
{
    public interface IBulletView : IDestroyable, IObjectView
    {
        event EventHandler<IObjectView> OnHit;
    }
}