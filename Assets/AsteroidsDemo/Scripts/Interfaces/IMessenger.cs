using System;

namespace AsteroidsDemo.Scripts.Interfaces
{
    public interface IMessenger : IService
    {
        void Publish<T>(T message);
        void Subscribe<T>(Action<T> action, Func<T, bool> precondition = null);
        void Unsubscribe<T>(Action<T> action);
        void UnsubscribeAll();
    }
}