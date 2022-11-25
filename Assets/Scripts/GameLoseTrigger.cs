using Ball;
using Contexts.Level.Signals;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class GameLoseTrigger : MonoBehaviour
{
    private SignalBus _signalBus;
    private readonly CompositeDisposable _disposable = new CompositeDisposable();

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    private void Start()
    {
        this.OnTriggerEnter2DAsObservable().First(col => col.TryGetComponent(out BallFacade _)).Subscribe(OnEnter)
            .AddTo(_disposable);
    }

    private void OnEnter(Collider2D col)
    {
        _signalBus.Fire<GameLoseSignal>();
    }

    private void OnDestroy()
    {
        if (_disposable.Count > 0)
            _disposable.Dispose();
    }
}
