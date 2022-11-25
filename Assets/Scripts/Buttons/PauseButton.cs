using Windows.Pause;
using Contexts.Level.Signals;
using Contexts.Project.Services;
using Cysharp.Threading.Tasks;
using Lean.Gui;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Buttons
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private LeanButton button;
        
        private IWindowsService _windowsService;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(IWindowsService windowsService, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _windowsService = windowsService;
        }

        private void Start()
        {
            button.OnClickAsObservableLimit().Subscribe(_ => Pause()).AddTo(this);
        }

        private async void Pause()
        {
            _signalBus.Fire<GamePausedSignal>();
            
            var window = await _windowsService.CreateSingle<PauseWindow>();
            
            window.Show().Forget();
        }
    }
}
