using UnityEngine;

namespace AsteroidsDemo.Entities.Weapon.Laser
{
    public class LaserView : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            // _lineRenderer.startWidth = 0.05f;
            // _lineRenderer.endWidth = 0.01f;
        }

        public void DrawLaser(Vector3 start, Vector3 end)
        {
            _lineRenderer.SetPositions(new[]
            {
                start, end
            });
        }

        public void Erase()
        {

            _lineRenderer.SetPositions(new Vector3[] {Vector3.zero, Vector3.zero});
        }
    }
}