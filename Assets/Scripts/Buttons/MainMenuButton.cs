using Contexts.Level.Signals;
using Lean.Gui;
using UniRx;
using UnityEngine;
using Utils;
using Zenject;

namespace Buttons
{
    public class MainMenuButton : MonoBehaviour
    {
        [SerializeField] private LeanButton button;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            button.OnClickAsObservableLimit().Subscribe(_ => _signalBus.Fire<LoadMenuSignal>()).AddTo(this);
        }
    }
}
