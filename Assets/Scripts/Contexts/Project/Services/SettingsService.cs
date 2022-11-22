using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using ScriptableObjects.Settings.Base;
using ScriptableObjects.Settings.Base.Interfaces;
using Zenject;

namespace Contexts.Project.Services
{
    public interface ISettingsService
    {
        ISetting<TType> GetSetting<TType, TSettingType>() where TSettingType : ISetting<TType>;
        ISavedSetting<TType> GetSavableSetting<TType, TSettingType>() where TSettingType : ISetting<TType>;
    }

    public class SettingsService : IInitializable, ISettingsService
    {
        private Dictionary<Type, ILoadedSetting> _loadedSettings;
        private readonly IList<SettingBase> _settings;

        public SettingsService(List<SettingBase> settings)
        {
            _settings = settings;
        }
        
        void IInitializable.Initialize()
        {
            _loadedSettings = _settings.ToDictionary(x => x.GetType(), x => x as ILoadedSetting);
        }

        public void LoadSettings()
        {
            foreach (var setting in _loadedSettings)
            {
                setting.Value.Load();
            }
        }

        public ISetting<TType> GetSetting<TType, TSettingType>() where TSettingType : ISetting<TType>
        {
            return Get<TType, TSettingType>();
        }

        public ISavedSetting<TType> GetSavableSetting<TType, TSettingType>() where TSettingType : ISetting<TType>
        {
            return Get<TType, TSettingType>() as ISavedSetting<TType>;
        }

        private ISetting<TType> Get<TType, TSettingType>() where TSettingType : ISetting<TType>
        {
            return _loadedSettings[typeof(TSettingType)] as ISetting<TType>;
        }
    }
}
