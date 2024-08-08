using System;


namespace DataSystem
{
    [Serializable]
    public class SettingsData
    {
        public bool soundOn = true;
        public bool hapticOn = true;
        
        public bool SoundOn
        {
            get => soundOn;
            set => soundOn = value;
        }

        public bool HapticOn
        {
            get => hapticOn;
            set => hapticOn = value;
        }
    }
}