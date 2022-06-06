using AsteroidsDemo.Scripts.Interfaces.View;

namespace AsteroidsDemo.Scripts.Messaging.Messages
{
    public class HitMessage
    {
        public IObjectView View { get; set; }
    }
}