using System;
using System.Collections.Generic;
using System.Linq;
using AsteroidsDemo.Scripts.Interfaces.Services;

namespace AsteroidsDemo.Scripts.Services.Messaging
{
    public class SimpleMessenger : IMessenger
    {
        private readonly List<KeyValuePair<Type, Subscription>> _subscriptions = new();

        public void Publish<T>(T message)
        {
            var messageType = message.GetType();

            var subscriptions = _subscriptions.ToArray();

            for (int i = 0; i < subscriptions.Length; i++)
            {
                var (key, value) = subscriptions[i];
                if ((messageType == key || messageType.IsSubclassOf(key)) &&
                    value.Precondition(message))
                {
                    value.Callback(message);
                }
            }
        }

        public void Subscribe<T>(Action<T> action, Func<T, bool> precondition = null)
        {
            _subscriptions.Add(
                new KeyValuePair<Type, Subscription>(
                    typeof(T),
                    Subscription.Create(action, precondition)));
        }

        private static Action<object> Convert<T>(Action<T> myActionT)
        {
            if (myActionT == null) return null;
            return o => myActionT((T) o);
        }

        private static Func<object, bool> Convert<T>(Func<T, bool> myActionT)
        {
            if (myActionT == null) return _ => true;
            return o => myActionT((T) o);
        }

        public void Unsubscribe<T>(Action<T> action)
        {
            var subscription = _subscriptions.FirstOrDefault(x => x.Value.Id == action.GetHashCode());
            if (subscription.Key != null)
            {
                _subscriptions.Remove(subscription);
            }
        }

        public void UnsubscribeAll()
        {
            _subscriptions.Clear();
        }

        private class Subscription
        {
            public int Id { get; }
            public Action<object> Callback { get; }
            public Func<object, bool> Precondition { get; }

            public static Subscription Create<T>(Action<T> callback, Func<T, bool> precondition)
            {
                return new(callback.GetHashCode(), Convert(callback), Convert(precondition));
            }

            private Subscription(int id, Action<object> callback, Func<object, bool> precondition)
            {
                Id = id;
                Callback = Convert(callback);
                Precondition = precondition;
            }
        }
    }
}