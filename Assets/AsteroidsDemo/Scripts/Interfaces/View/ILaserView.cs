using UnityEngine;

namespace AsteroidsDemo.Scripts.Interfaces.View
{
    public interface ILaserView
    {
        void DrawLaser(Vector3 start, Vector3 end);
        void Erase();
    }
}