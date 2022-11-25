using System;
using Contexts.Level.Signals;
using Contexts.Project.Services.Progress;
using Contexts.Project.Services.Progress.Data.Transitions;
using UniRx;
using Zenject;

namespace Contexts.Level.Services
{
    public class LevelGameProgressSignalsListener : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IGameProgressService _gameProgressService;
        private readonly IGameProgressLoader _loader;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public LevelGameProgressSignalsListener(SignalBus signalBus, IGameProgressService gameProgressService, IGameProgressLoader loader)
        {
            _signalBus = signalBus;
            _gameProgressService = gameProgressService;
            _loader = loader;
        }
        
        public void Initialize()
        {
            _signalBus.GetStream<StarPickedSignal>().Subscribe(_ => OnStarPicked()).AddTo(_disposable);
            
            _signalBus.GetStream<BallHitTheBasketSignal>().Subscribe(OnBallHitBasket)
                .AddTo(_disposable);
        }

        private void OnBallHitBasket(BallHitTheBasketSignal signal)
        {
            _gameProgressService.MakeTransition(new AddScoreTransition(signal.Combo));
        }

        private void OnStarPicked()
        {
            _gameProgressService.MakeTransition(new IncrementStarsTransition());
            _loader.Save();
        }

        public void Dispose()
        {
            _gameProgressService.MakeTransition(new ResetScoreTransition());
            _disposable.Dispose();
        }
    }
}