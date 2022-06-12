using System;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Interfaces.View;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.View.PortableObject
{
    public class PortableObjectView : MonoBehaviour, IObjectView
    {
        public Vector3 Position => transform.position;
        public Vector3 Direction => transform.up;
        public Vector3 EulerAngles => transform.eulerAngles;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Vector3 eulerAngles)
        {
            transform.eulerAngles = eulerAngles;
        }

        public event EventHandler ViewStarted;

        protected void Start()
        {
            ViewStarted?.Invoke(this, EventArgs.Empty);
        }
    }
}