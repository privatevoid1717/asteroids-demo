using AsteroidsDemo.Scripts.Interfaces.Model;
using AsteroidsDemo.Scripts.Messages;
using TMPro;
using UnityEngine;

namespace AsteroidsDemo.Scripts.UI
{
    public class GameOver : UIComponent
    {
        [SerializeField] private TextMeshProUGUI score;

        private ISpaceShipModel _playerModel;

        private void Start()
        {
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
            Messenger.Publish(new NewGameMessage());
        }

        protected override void Subscribe()
        {
            Messenger.Subscribe<PlayerSpawnedMessage>(OnPlayerSpawned);
            Messenger.Subscribe<PlayerDestroyedMessage>(OnPlayerDestroyed);
        }
    }
}