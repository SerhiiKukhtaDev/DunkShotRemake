using Contexts.Project.Services;
using Cysharp.Threading.Tasks;
using Lean.Gui;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Windows.Settings
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private LeanButton button;
        private IWindowsService _windowsService;

        [Inject]
        private void Construct(IWindowsService windowsService)
        {
            _windowsService = windowsService;
        }
        
        private void Start()
        {
            button.OnClickAsObservableLimit().Subscribe(_ => ShowSettingsWindow().Forget()).AddTo(this);
        }

        private async UniTaskVoid ShowSettingsWindow()
        {
            Debug.Log("Atata");
            var window = await _windowsService.CreateSingle<SettingsWindow>();
            window.Show().Forget();
        }
    }
}
