using Contexts.Project.Services;
using ScriptableObjects;
using ScriptableObjects.Settings.Base;
using ScriptableObjects.Settings.Base.Interfaces;
using UniRx;
using UnityEngine;
using Zenject;

public class Background : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private BackgroundSettings backgroundSettings;

    private ISettingsService _settingsService;
    private ISetting<bool> _useNightModeSetting;

    [Inject]
    private void Construct(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }
    
    private void Start()
    {
        _useNightModeSetting = _settingsService.GetSetting<bool, UseNightModeSetting>();

        _useNightModeSetting.Setting.Subscribe(value =>
        {
            camera.backgroundColor = value ? backgroundSettings.BlackThemeColor : backgroundSettings.LightThemeColor;
        }).AddTo(this);
    }
}
