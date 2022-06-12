using UnityEngine;

namespace AsteroidsDemo.Scripts.Messages
{
    public abstract class DestroyedMessage
    {
        public Vector3 Position { get; set; }
    }
}