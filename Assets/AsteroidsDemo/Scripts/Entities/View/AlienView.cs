using System;
using AsteroidsDemo.Infrastructure.Scripts.PortableObject;
using AsteroidsDemo.Scripts.Interfaces.View;
using UnityEngine;

namespace AsteroidsDemo.Common.Scripts
{
    public class AlienView : PortableObjectView, IAlienView
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            // if (other.CompareTag("Bullet"))
            // {
            //     OnBulletHit?.Invoke(this, EventArgs.Empty);
            // }
        }

        public event EventHandler OnBulletHit;
    }
}