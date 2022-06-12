using AsteroidsDemo.Scripts.Entities.View.PortableObject;
using AsteroidsDemo.Scripts.Interfaces.View;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.View
{
    public class AsteroidView : PortableObjectView, IAsteroidView
    {
        public void SetScale(float scale)
        {
            transform.localScale = new Vector3(scale, scale, 1f);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}