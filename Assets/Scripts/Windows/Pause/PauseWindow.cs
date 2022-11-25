using Contexts.Level.Signals;
using Core;
using Cysharp.Threading.Tasks;
using Lean.Gui;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Windows.Pause
{
    public class PauseWindow : Window
    {
        [SerializeField] private LeanButton close;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void Start()
        {
            close.OnClickAsObservableLimit().Subscribe(_ => HideAndResume().Forget()).AddTo(this);
        }

        private async UniTaskVoid HideAndResume()
        {
            await Hide();
            _signalBus.AbstractFire<GameResumedSignal>();
        }
    }
}
