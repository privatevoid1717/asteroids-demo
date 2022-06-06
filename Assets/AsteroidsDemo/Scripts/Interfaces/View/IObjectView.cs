using System;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Interfaces.View
{
    public interface IObjectView
    {
        Vector3 Position { get; }
        Vector3 Direction { get; }

        Vector3 EulerAngles { get; }

        void SetPosition(Vector3 position);
        void SetRotation(Vector3 eulerAngles);

        public event EventHandler ViewStarted;
    }
}