using System;
using UnityEngine;

namespace ScriptableObjects.Settings.Base.Types
{
    public abstract class EnumSettingBase<TEnum> : SettingBase<TEnum>
    {
        public override void Load()
        {
            var value = PlayerPrefs.GetString(Key, DefaultValue.ToString());
            Setting.Value = (TEnum) Enum.Parse(typeof(TEnum), value);
        }
            
        public override void Save()
        {
            PlayerPrefs.SetString(Key, Setting.Value.ToString());
        }
    }
}