using System;
using Contexts.Level.Signals;
using Contexts.Project.Services.Progress.Data;
using UniRx;
using Zenject;

namespace Contexts.Project.Services.Progress
{
    public interface IGameProgressLoader
    {
        void Load();
        void Save();
    }

    public interface IGameProgressService
    {
        GameProgressReactive GameProgress { get; }
    }

    public partial class GameProgressService : IInitializable, IDisposable, IGameProgressLoader, IGameProgressService
    {
        private readonly SignalBus _signalBus;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        
        private GameProgressReactive _gameProgress;

        public GameProgressReactive GameProgress => _gameProgress;

        public GameProgressService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus.GetStream<StarPickedSignal>().Subscribe(_ => UpdateStars()).AddTo(_disposable);
        }

        private void UpdateStars()
        {
            _gameProgress.Stars.Value += 1;
            Save();
        }

        public void Dispose()
        {
            
        }
    }
}
