using System;
using System.Collections.Generic;
using System.Linq;
using AsteroidsDemo.Scripts.Tools.Collections;

namespace AsteroidsDemo.Scripts.Messaging
{
    public static class SimpleMessenger
    {
        private static readonly List<KeyValuePair<Type, Subscription>> Subscriptions = new();

        public static void Publish<T>(T message)
        {
            var messageType = message.GetType();

            for (int i = 0; i < Subscriptions.Count; i++)
            {
                var callback = Subscriptions[i];

                if (!callback.Value.IsActive)
                {
                    Subscriptions.RemoveAt(i);
                    i--;
                }
            }

            Subscriptions
                .Where(a => (messageType == a.Key || messageType.IsSubclassOf(a.Key)) &&
                            a.Value.Precondition(message))
                .ForEach(act => act.Value.Callback(message));
        }

        public static void Subscribe<T>(Action<T> action, Func<T, bool> precondition = null)
        {
            Subscriptions.Add(
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

        public static void Unsubscribe<T>(Action<T> action)
        {
            var subscription = Subscriptions.FirstOrDefault(x => x.Value.Id == action.GetHashCode());
            if (subscription.Key != null)
            {
                subscription.Value.IsActive = false;
            }
        }

        public static void UnsubscribeAll()
        {
            Subscriptions.ForEach(s => s.Value.IsActive = false);
        }

        private class Subscription
        {
            public int Id { get; }
            public Action<object> Callback { get; }
            public Func<object, bool> Precondition { get; }

            public bool IsActive { get; set; } = true;

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