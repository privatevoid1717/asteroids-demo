using UnityEngine;

namespace AsteroidsDemo.Scripts.Messages
{
    public class FireMessage
    {
        public Vector3 Position { get; set; }
        public Vector3 EulerAngles { get; set; }
    }
}