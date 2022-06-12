using AsteroidsDemo.Scripts.Data;
using AsteroidsDemo.Scripts.Interfaces;
using AsteroidsDemo.Scripts.Messages;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidsDemo.Scripts.Effects
{
    public class VfxPlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionEffect;

        private IMessenger _messenger;

        public VfxPlayer WithMessenger(IMessenger messenger)
        {
            _messenger = messenger;
            return this;
        }

        public VfxPlayer WithPrefabs(PrefabData data)
        {
            explosionEffect = data.ExplosionEffect;
            return this;
        }

        private void Start()
        {
            _messenger.Subscribe<DestroyedMessage>(OnDestroyed);
        }

        private void OnDestroyed(DestroyedMessage message)
        {
            PlayExplosionEffect(message.Position);
        }

        private void PlayExplosionEffect(Vector3 position)
        {
            var effect =
                Instantiate(explosionEffect, position,
                    Quaternion.Euler(0, 0, Random.Range(0, 360))); // TODO По хорошему нужно кэшировать
            Destroy(effect.gameObject, 2f);
        }
    }
}