using System;
using Contexts.Project.Services;
using ScriptableObjects.Settings.Base;
using ScriptableObjects.Settings.Base.Interfaces;
using UnityEngine;
using Zenject;

namespace Windows.Settings
{
    public class SoundsCanPlayToggle : MonoBehaviour
    {
        private ISettingsService _settingsService;
        private ISavedSetting<bool> _savableSetting;

        [Inject]
        private void Construct(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        
        private void Start()
        {
            _savableSetting = _settingsService.GetSavableSetting<bool, PlaySoundsSetting>();
        }

        public void Toggle()
        {
            _savableSetting.Setting.Value = !_savableSetting.Setting.Value;
            _savableSetting.Save();
        }
    }
}
