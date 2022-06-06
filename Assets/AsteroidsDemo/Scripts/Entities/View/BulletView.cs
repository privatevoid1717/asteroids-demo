using System;
using AsteroidsDemo.Common.Scripts;
using AsteroidsDemo.Infrastructure.Scripts.PortableObject;
using AsteroidsDemo.Scripts.Interfaces.View;
using UnityEngine;

namespace AsteroidsDemo.SpaceShip.Scripts.View
{
    public class BulletView : PortableObjectView
    {
        public event EventHandler<IObjectView> OnHit;

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Alien") || other.CompareTag("asteroid"))
            {
                OnHit?.Invoke(this, other.GetComponent<IObjectView>());
            }
        }
    }
}