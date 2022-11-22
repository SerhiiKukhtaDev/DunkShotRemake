using Contexts.Project.Services;
using DG.Tweening;
using ScriptableObjects.Settings.Base;
using ScriptableObjects.Settings.Base.Interfaces;
using UnityEngine;
using Zenject;

public class Background : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Color lightThemeColor;
    [SerializeField] private Color blackThemeColor;
    
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
        camera.backgroundColor = _useNightModeSetting.Setting.Value ? blackThemeColor : lightThemeColor;
    }
}
