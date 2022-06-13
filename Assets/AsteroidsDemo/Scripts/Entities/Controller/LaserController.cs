using AsteroidsDemo.Scripts.Interfaces.Services;
using AsteroidsDemo.Scripts.Interfaces.View;
using AsteroidsDemo.Scripts.Messages;
using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.Controller
{
    public class LaserController
    {
        private const float Distance = 7f;

        private bool _isFiring;
        private Vector3 _position;
        private Vector3 _direction;
        private readonly ILaserView _view;
        private LayerMask _borderLayer = LayerMask.GetMask("ScreenBorder");
        private readonly IMessenger _messenger;

        public LaserController(ILaserView view, IMessenger messenger)
        {
            _view = view;
            _messenger = messenger;
        }

        public void Hold()
        {
            _view.Erase();
        }


        // TODO Refactoring
        public void Fire(Vector3 position, Vector3 direction)
        {
            var ray = new Ray2D(position, direction * Distance);
            _view.DrawLaser(position, ray.GetPoint(Distance));

            var hit = Physics2D.Raycast(position, direction, Distance,
                LayerMask.GetMask("Target") | LayerMask.GetMask("ScreenBorder"));

            if (hit.collider)
            {
                if (hit.collider.CompareTag("Alien") || hit.collider.CompareTag("asteroid"))
                {
                    _messenger.Publish(new HitMessage()
                    {
                        View = hit.collider.GetComponent<IObjectView>()
                    });
                    return;
                }

                if (hit.collider.CompareTag("ScreenBorder"))
                {
                    ray = new Ray2D(hit.point, hit.collider.transform.up * 20f);

                    var hits2 = Physics2D.RaycastAll(hit.point, ray.direction, 20f, _borderLayer.value); // TODO NoAloc

                    for (int i = 0; i < hits2.Length; i++)
                    {
                        var hit2 = hits2[i];

                        if (hit2.collider == hit.collider)
                        {
                            continue;
                        }

                        if (hit2.collider)
                        {
                            var hit3 = Physics2D.Raycast(hit2.point, direction, Distance - hit.distance,
                                LayerMask.GetMask("Target"));

                            if (hit3.collider)
                            {
                                if (hit3.collider.CompareTag("Alien") || hit3.collider.CompareTag("asteroid"))
                                {
                                    _messenger.Publish(new HitMessage()
                                    {
                                        View = hit3.collider.GetComponent<IObjectView>()
                                    });
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}