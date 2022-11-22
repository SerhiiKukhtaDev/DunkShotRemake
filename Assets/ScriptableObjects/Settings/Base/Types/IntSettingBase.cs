using UnityEngine;

namespace ScriptableObjects.Settings.Base.Types
{
    public abstract class IntSettingBase : SettingBase<int>
    {
        public override void Save()
        {
            PlayerPrefs.SetInt(Key, Setting.Value);
        }
    
        public override void Load()
        {
            Setting.Value = PlayerPrefs.GetInt(Key, DefaultValue);
        }
    }
}