using System.Text;
using AsteroidsDemo.Scripts.Interfaces.Model;
using AsteroidsDemo.Scripts.Messages;
using TMPro;
using UnityEngine;

namespace AsteroidsDemo.Scripts.UI
{
    public class Hud : UIComponent
    {
        [field: SerializeField] private TextMeshProUGUI coords;

        private ISpaceShipModel _playerModel;
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        
        private void Awake()
        {
           
        }

        private void OnDisable()
        {
            Messenger.Unsubscribe<PlayerSpawnedMessage>(OnPlayerSpawned);
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

        protected override void Subscribe()
        {
            Messenger.Subscribe<PlayerSpawnedMessage>(OnPlayerSpawned);
        }
    }
}