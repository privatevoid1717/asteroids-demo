using System.Text;
using AsteroidsDemo.Scripts.Entities.Model;
using AsteroidsDemo.Scripts.Messaging;
using AsteroidsDemo.Scripts.Messaging.Messages;
using TMPro;
using UnityEngine;

namespace AsteroidsDemo.Scripts.UI
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI coords;

        private SpaceShipModel _playerModel;
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        private void OnEnable()
        {
            SimpleMessenger.Subscribe<PlayerSpawnedMessage>(OnPlayerSpawned);
        }

        private void OnDisable()
        {
            SimpleMessenger.Unsubscribe<PlayerSpawnedMessage>(OnPlayerSpawned);
        }

        private void OnPlayerSpawned(PlayerSpawnedMessage obj)
        {
            _playerModel = obj.PlayerModel;
        }

        private void Update()
        {
            _stringBuilder.Clear();

            _stringBuilder
                .Append("Координаты:")
                .Append((Vector2) _playerModel.Position)
                .Append("  ")
                .Append("Угол:")
                .Append((360 - _playerModel.EulerAngles.z).ToString("0"))
                .Append("  ")
                .Append("Скорость:")
                .Append(_playerModel.Speed.ToString("0.000"))
                .Append("  ")
                .Append("Заряд лазера:")
                .Append(_playerModel.Energy.ToString("0"))
                .Append("  ")
                .Append("Откат лазера:")
                .Append(_playerModel.Cooldown.ToString("0.0"))
                .Append("  ");

            coords.text = _stringBuilder.ToString();
        }
    }
}