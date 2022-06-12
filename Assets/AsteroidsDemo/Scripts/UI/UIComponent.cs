using AsteroidsDemo.Scripts.Interfaces;
using UnityEngine;

namespace AsteroidsDemo.Scripts.UI
{
    public abstract class UIComponent : MonoBehaviour
    {
        protected IMessenger Messenger { get; private set; }

        public UIComponent WithMessenger(IMessenger messenger)
        {
            Messenger = messenger;
            Subscribe();
            return this;
        }

        protected abstract void Subscribe();
    }
}