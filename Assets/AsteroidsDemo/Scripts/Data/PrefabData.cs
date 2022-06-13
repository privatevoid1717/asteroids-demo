using AsteroidsDemo.Scripts.Entities.View;
using AsteroidsDemo.Scripts.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AsteroidsDemo.Scripts.Data
{
    [CreateAssetMenu(fileName = "PrefabData", menuName = "Data", order = 0)]
    public class PrefabData : ScriptableObject // TODO разбить на разные классы
    {
        #region UI

        [field: SerializeField] public Hud Hud { get; private set; }
        [field: SerializeField] public GameOver GameOver { get; private set; }

        #endregion

        #region Entities

        [field: SerializeField] public AsteroidView AsteroidPrefab { get; private set; }
        [field: SerializeField] public PlayerShipView PlayerShipPrefab { get; private set; }
        [field: SerializeField] public BulletView BulletPrefab { get; private set; }
        [field: SerializeField] public LaserView LaserPrefab { get; private set; }
        [field: SerializeField] public AlienView AlienPrefab { get; private set; }

        #endregion

        #region Infrastructure

        [field: SerializeField] public GameObject SpawnerPrefab { get; private set; }
        [field: SerializeField] public PlayerInput PlayerInputPrefab { get; private set; }

        #endregion

        #region Effects

        [field: SerializeField] public ParticleSystem ExplosionEffect { get; private set; }
        [field: SerializeField] public GameObject VfxPlayer { get; private set; }

        #endregion
    }
}