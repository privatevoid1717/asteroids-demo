using AsteroidsDemo.Scripts.Entities.View.PortableObject;
using AsteroidsDemo.Scripts.Interfaces.View;

namespace AsteroidsDemo.Scripts.Entities.View
{
    public class AlienView : PortableObjectView, IAlienView
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}