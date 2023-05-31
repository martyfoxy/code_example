using Code.Data;
using UnityEngine;

namespace Code.Settings
{
    /// <summary>
    /// Предоставляет десериализованные настройки игры
    /// </summary>
    public sealed class SettingsProvider
    {
        private const string FileName = "data";
        
        public RootDataModel Root
        {
            get
            {
                if (_settings != null)
                    return _settings;
                
                var asset = Resources.Load(FileName) as TextAsset;
                if (asset == null)
                {
                    Debug.LogError($"Couldn't find settings file {FileName} in Resources");
                    return null;
                }

                _settings = JsonUtility.FromJson<RootDataModel>(asset.text);
                return _settings;
            }
        }

        private RootDataModel _settings;
    }
}