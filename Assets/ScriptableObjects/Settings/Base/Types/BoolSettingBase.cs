using UnityEngine;

namespace ScriptableObjects.Settings.Base.Types
{
    public abstract class BoolSettingBase : SettingBase<bool>
    {
        public override void Load()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                Setting.Value = PlayerPrefs.GetInt(Key) == 1;
            }
            else
            {
                Setting.Value = DefaultValue;
            }
        }
            
        public override void Save()
        {
            PlayerPrefs.SetInt(Key, Setting.Value ? 1 : 0);
        }
    }
}