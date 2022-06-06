using AsteroidsDemo.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace AsteroidsDemo.Scripts.CameraManagement
{
    public class TiledCamera : MonoBehaviour, ITiledCamera
    {
        public Vector3[] offsets;
        public int Width => _mainCamera.scaledPixelWidth;
        public int Height => _mainCamera.scaledPixelHeight;

        public Camera MainCamera => _mainCamera;

        private Camera _mainCamera;


        [SerializeField] private Camera overlayCameraPrefab;

        private void Start()
        {
            _mainCamera = GetComponent<Camera>();

            offsets = new[]
            {
                new Vector3(0, -Height, 0),
                new Vector3(0, Height, 0),
                new Vector3(-Width, 0, 0),
                new Vector3(Width, 0, 0),
                new Vector3(-Width, -Height, 0),
                new Vector3(Width, Height, 0),
                new Vector3(-Width, Height, 0),
                new Vector3(Width, -Height, 0)
            };

            GenerateTiledCameras();
        }

        private void GenerateTiledCameras()
        {
            var cameraData = _mainCamera.GetUniversalAdditionalCameraData();

            foreach (var offset in offsets)
            {
                var overlayCamera = Instantiate(overlayCameraPrefab, transform);
                var camPosition = _mainCamera
                    .ScreenToWorldPoint(
                        _mainCamera.WorldToScreenPoint(_mainCamera.transform.position) + offset);

                overlayCamera.transform.position = camPosition;
                cameraData.cameraStack.Add(overlayCamera);
            }
        }
    }
}