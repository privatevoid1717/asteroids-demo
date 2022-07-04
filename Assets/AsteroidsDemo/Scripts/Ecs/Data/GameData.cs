using AsteroidsDemo.Scripts.CameraManagement;
using AsteroidsDemo.Scripts.Entities.View;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AsteroidsDemo.Scripts.Ecs.Data
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public PlayerShipView PlayerShipViewPrefab { get; private set; }
        [field: SerializeField] public AsteroidView AsteroidViewPrefab { get; private set; }
        [field: SerializeField] public AlienView AlienViewPrefab { get; private set; }

        [field: SerializeField] public PlayerInput PlayerInput { get; private set; }

        [field: SerializeField] public TiledCamera TiledCamera { get; private set; }
    }
}