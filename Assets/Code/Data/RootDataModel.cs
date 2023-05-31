using System;

namespace Code.Data
{
    [Serializable]
    public class RootDataModel
    {
        public CameraSettingsModel cameraSettings;
        public GameSettingsModel settings;
        public StatModel[] stats;
        public BuffModel[] buffs;
    }

    [Serializable]
    public class CameraSettingsModel
    {
        public float roundDuration;
        public float roundRadius;
        public float height;
        public float lookAtHeight;
        public float roamingRadius;
        public float roamingDuration;
        public float fovMin;
        public float fovMax;
        public float fovDelay;
        public float fovDuration;
    }

    [Serializable]
    public class GameSettingsModel
    {
        public int playersCount;
        public int buffCountMin;
        public int buffCountMax;
        public bool allowDuplicateBuffs;
    }

    [Serializable]
    public class StatModel
    {
        public int id;
        public string title;
        public string icon;
        public float value;
    }
    
    [Serializable]
    public class BuffModel
    {
        public string icon;
        public int id;
        public string title;
        public BuffStatModel[] stats;
    }
    
    [Serializable]
    public class BuffStatModel
    {
        public float value;
        public int statId;
    }
}