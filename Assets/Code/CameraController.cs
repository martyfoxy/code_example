using System;
using System.Threading;
using Code.Settings;
using Code.Utils;
using Zenject;
using Random = UnityEngine.Random;

namespace Code
{
    /// <summary>
    /// Управляет камерой
    /// </summary>
    public sealed class CameraController : IInitializable, IDisposable, ITickable
    {
        private const float CameraSpeed = 10f;
        
        private readonly CameraView _cameraView;
        private readonly SettingsProvider _settingsProvider;
        private CancellationTokenSource _cts;

        public CameraController(
            CameraView cameraView,
            SettingsProvider settingsProvider)
        {
            _cameraView = cameraView;
            _settingsProvider = settingsProvider;
        }
        
        public void Initialize()
        {
            _cameraView.Setup(_settingsProvider.Root.cameraSettings);

            StartZoomingAsync();
        }
        
        public void Dispose()
        {
            _cts?.Cancel();
        }

        public void Tick()
        {
            _cameraView.Rotate(CameraSpeed);
        }

        private async void StartZoomingAsync()
        {
            _cts = new CancellationTokenSource();
            
            while (true)
            {
                if (_cts.IsCancellationRequested)
                    return;
                
                var randonDelay = Random.Range(1, _settingsProvider.Root.cameraSettings.fovDelay);
                await AsyncUtils.WaitForSeconds(randonDelay);
                
                _cameraView.SwitchZoom();
                
                var randonDuration = Random.Range(1, _settingsProvider.Root.cameraSettings.fovDuration);
                await AsyncUtils.WaitForSeconds(randonDuration);
                
                _cameraView.SwitchZoom();
            }
        }
    }
}