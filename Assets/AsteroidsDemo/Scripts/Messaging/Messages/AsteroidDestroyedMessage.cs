namespace AsteroidsDemo.Scripts.Messaging.Messages
{
    public class AsteroidDestroyedMessage : DestroyedMessage
    {
        public bool IsDebris { get; set; }
    }
}