using System;
using AsteroidsDemo.Common.Scripts;
using AsteroidsDemo.Infrastructure.Scripts.PortableObject;
using AsteroidsDemo.Scripts.Interfaces.View;
using UnityEngine;

namespace AsteroidsDemo.SpaceShip.Scripts.View
{
    public class AsteroidView : PortableObjectView, IAsteroidView
    {
        public event EventHandler OnBulletHit;

        public void SetScale(float scale)
        {
            transform.localScale = new Vector3(scale, scale, 1f);
        }

        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.CompareTag("Bullet"))
        //     {
        //         OnBulletHit?.Invoke(this, EventArgs.Empty);
        //     }
        // }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}