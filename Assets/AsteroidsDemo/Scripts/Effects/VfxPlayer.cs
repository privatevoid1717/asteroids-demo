using AsteroidsDemo.Scripts.Messaging;
using AsteroidsDemo.Scripts.Messaging.Messages;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidsDemo.Scripts.Effects
{
    public class VfxPlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionEffect;

        private void OnEnable()
        {
            SimpleMessenger.Subscribe<DestroyedMessage>(OnDestroyed);
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