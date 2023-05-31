using System.Collections;
using Code.Data;
using UnityEngine;

namespace Code
{
    /// <summary>
    /// Обертка над камерой
    /// </summary>
    public sealed class CameraView : MonoBehaviour
    {
        [SerializeField]
        private Camera camera;
        
        private const float ZoomSpeed = 50f;
        
        private bool _isZoomed;
        private float _fovMin;
        private float _fovMax;
        private Coroutine _zoomCoroutine;

        public void Setup(CameraSettingsModel cameraSettingsModel)
        {
            _fovMin = cameraSettingsModel.fovMin;
            _fovMax = cameraSettingsModel.fovMax;
            
            SetFov(cameraSettingsModel.fovMax);
        }

        public void Rotate(float speed)
        {
            transform.Rotate(0f, speed * Time.deltaTime, 0f);
        }
        
        public void SwitchZoom()
        {
            if (_zoomCoroutine != null)
                StopCoroutine(_zoomCoroutine);

            if (_isZoomed)
                _zoomCoroutine = StartCoroutine(ZoomOutCoroutine());
            else
            {
                var randomFov = _fovMin + Random.Range(0f, 20f);
                _zoomCoroutine = StartCoroutine(ZoomInCoroutine(randomFov));
            }
        }
        
        private IEnumerator ZoomInCoroutine(float endValue)
        {
            var currentFov = camera.fieldOfView;

            while (currentFov > endValue)
            {
                currentFov -= ZoomSpeed * Time.deltaTime;
                SetFov(currentFov);
                yield return null;
            }

            _isZoomed = true;
        }
        
        private IEnumerator ZoomOutCoroutine()
        {
            var currentFov = camera.fieldOfView;
            
            while (currentFov < _fovMax)
            {
                currentFov += ZoomSpeed * Time.deltaTime;
                SetFov(currentFov);
                yield return null;
            }
            
            _isZoomed = false;
        }
        
        private void SetFov(float value)
        {
            camera.fieldOfView = Mathf.Clamp(value, _fovMin, _fovMax);
        }
    }
}
