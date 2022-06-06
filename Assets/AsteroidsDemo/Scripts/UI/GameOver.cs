using AsteroidsDemo.Scripts.Entities.Model;
using AsteroidsDemo.Scripts.Messaging;
using AsteroidsDemo.Scripts.Messaging.Messages;
using TMPro;
using UnityEngine;

namespace AsteroidsDemo.Scripts.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI score;

        private SpaceShipModel _playerModel;

        private void Awake()
        {
            SimpleMessenger.Subscribe<PlayerSpawnedMessage>(OnPlayerSpawned);
            SimpleMessenger.Subscribe<PlayerDestroyedMessage>(OnPlayerDestroyed);

            gameObject.SetActive(false);
        }

        private void OnPlayerDestroyed(PlayerDestroyedMessage message)
        {
            score.text = "Вы проиграли со счетом " + _playerModel.Score;
            gameObject.SetActive(true);
        }

        private void OnPlayerSpawned(PlayerSpawnedMessage message)
        {
            _playerModel = message.PlayerModel;
        }

        public void NewGame()
        {
            SimpleMessenger.Publish(new NewGameMessage());
        }
    }
}