using UnityEngine;

namespace AsteroidsDemo.Scripts.Interfaces.Model
{
    public interface ISpaceShipModel : IModel
    {
        float Speed { get; set; }
        int Score { get; set; }
        float MaxEnergy { get; set; }
        float MaxCooldown { get; set; }
        float Energy { get; set; }
        float Cooldown { get; set; }
    }
}