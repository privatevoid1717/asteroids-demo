using UnityEngine;

namespace AsteroidsDemo.Scripts.Entities.View
{
    public class LaserView : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
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