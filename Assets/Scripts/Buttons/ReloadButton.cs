using Contexts.Level.Signals;
using Lean.Gui;
using UniRx;
using UnityEngine;
using Zenject;

namespace Buttons
{
    public class ReloadButton : MonoBehaviour
    {
        [SerializeField] private LeanButton button;
        private SignalBus _signalBus;
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void Start()
        {
            button.OnClick.AsObservable().First().Subscribe(_ => _signalBus.Fire<ReloadLevelSignal>()).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            if (_disposable.Count > 0)
                _disposable.Dispose();
        }
    }
}
