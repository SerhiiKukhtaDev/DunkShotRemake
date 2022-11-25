using System;
using Contexts.Project.Services;
using Lean.Gui;
using ScriptableObjects.Settings.Base;
using ScriptableObjects.Settings.Base.Interfaces;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Windows.Settings
{
    public class NightModeToggle : MonoBehaviour
    {
        private ISettingsService _settingsService;
        private ISavedSetting<bool> _useNightModeSetting;

        [Inject]
        private void Construct(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        
        private void Start()
        {
            _useNightModeSetting = _settingsService.GetSavableSetting<bool, UseNightModeSetting>();
        }

        public void Toggle()
        {
            _useNightModeSetting.Setting.Value = !_useNightModeSetting.Setting.Value;
            _useNightModeSetting.Save();
        }
    }
}
