using AsteroidsDemo.Scripts.Entities.Model;

namespace AsteroidsDemo.Scripts.Messaging.Messages
{
    public class PlayerSpawnedMessage
    {
        public SpaceShipModel PlayerModel { get; set; }
    }
}