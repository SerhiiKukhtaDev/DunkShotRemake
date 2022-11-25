using Contexts.Project.Services;
using ScriptableObjects;
using ScriptableObjects.Settings.Base;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BackgroundImage : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private BackgroundSettings backgroundSettings;
    
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
            image.color = value ? backgroundSettings.BlackThemeColor : backgroundSettings.LightThemeColor;
        }).AddTo(this);
    }
}