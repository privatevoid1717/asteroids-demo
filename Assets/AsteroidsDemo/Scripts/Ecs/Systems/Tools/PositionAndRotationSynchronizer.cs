using AsteroidsDemo.Scripts.Ecs.Components;
using AsteroidsDemo.Scripts.Physics;

namespace AsteroidsDemo.Scripts.Ecs.Systems.Tools
{
    public static class PositionAndRotationSynchronizer
    {
        public static void UpdateAndApply(ref PositionAndRotationComponent component, CustomRigidbody2 rigidbody)
        {
            var (deltaPosition, deltaAngle) = rigidbody.Update();
            component.Position += deltaPosition;
            component.Rotation += deltaAngle;
        }
    }
}