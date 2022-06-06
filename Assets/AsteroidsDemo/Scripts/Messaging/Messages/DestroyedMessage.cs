using UnityEngine;

namespace AsteroidsDemo.Scripts.Messaging.Messages
{
    public abstract class DestroyedMessage
    {
        public Vector3 Position { get; set; }
    }
}