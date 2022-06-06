using System;

namespace AsteroidsDemo.Scripts.Interfaces.View
{
    public interface IPlayerShipView : IObjectView
    {
        void SetForsage(bool enabled);
        public event EventHandler PlayerWasHit;
    }
}