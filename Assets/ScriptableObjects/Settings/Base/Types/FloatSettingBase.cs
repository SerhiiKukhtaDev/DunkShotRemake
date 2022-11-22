using UnityEngine;

namespace ScriptableObjects.Settings.Base.Types
{
    public abstract class FloatSettingBase : SettingBase<float>
    {
        public override void Save()
        {
            PlayerPrefs.SetFloat(Key, Setting.Value);
        }
    
        public override void Load()
        {
            Setting.Value = PlayerPrefs.GetFloat(Key, DefaultValue);
        }
    }
}