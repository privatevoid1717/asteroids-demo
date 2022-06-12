using System;
using AsteroidsDemo.Scripts.Entities.View.PortableObject;
using AsteroidsDemo.Scripts.Interfaces.View;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.View
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