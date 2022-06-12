using AsteroidsDemo.Scripts.Interfaces.Model;

namespace AsteroidsDemo.Scripts.Messages
{
    public class PlayerSpawnedMessage
    {
        public ISpaceShipModel PlayerModel { get; set; }
    }
}