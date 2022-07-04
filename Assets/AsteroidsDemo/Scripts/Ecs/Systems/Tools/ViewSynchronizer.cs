using AsteroidsDemo.Scripts.Ecs.Components;
using AsteroidsDemo.Scripts.Entities.View.PortableObject;
using AsteroidsDemo.Scripts.Physics;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Ecs.Systems.Tools
{
    public static class ViewSynchronizer
    {
        public static void Synchronize(ref PositionAndRotationComponent positionAndRotationComponent, PortableObjectView view)
        {
            view.SetPosition(positionAndRotationComponent.Position);
            view.SetRotation(new Vector3(0, 0, positionAndRotationComponent.Rotation));
        }
    }
}