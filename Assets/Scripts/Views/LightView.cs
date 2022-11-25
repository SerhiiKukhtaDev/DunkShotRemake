using System;
using Contexts.Project.Services;
using ScriptableObjects.Settings.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Views
{
    public class LightView : MonoBehaviour
    {
        [SerializeField] private Sprite lampOn;
        [SerializeField] private Sprite lampOff;
        [SerializeField] private Image image;
        
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
                image.sprite = value ? lampOff : lampOn;
            }).AddTo(this);
        }
    }
}
