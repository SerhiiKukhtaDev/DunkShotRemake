using System;
using Contexts.Project.Services;
using Lean.Gui;
using ScriptableObjects.Settings.Base;
using UniRx;
using UnityEngine;
using Zenject;

namespace Views
{
    public class NightModeToggleView : MonoBehaviour
    {
        [SerializeField] private LeanToggle toggle;
        private ISettingsService _settingsService;

        [Inject]
        private void Construct(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        
        private void Start()
        {
            var useNightModeSetting = _settingsService.GetSetting<bool, UseNightModeSetting>();

            useNightModeSetting.Setting.Subscribe(value =>
            {
                toggle.On = value;
            }).AddTo(this);
        }
    }
}
