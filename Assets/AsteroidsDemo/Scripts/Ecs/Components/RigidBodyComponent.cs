using AsteroidsDemo.Scripts.Physics;

namespace AsteroidsDemo.Scripts.Ecs.Components
{
    public struct RigidBodyComponent
    {
        public CustomRigidbody2 Rigidbody { get; set; }
    }
}