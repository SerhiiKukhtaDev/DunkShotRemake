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

        void MakeTransition(GameProgressTransition transition);
    }

    public partial class GameProgressService : IGameProgressLoader, IGameProgressService
    {
        private readonly SignalBus _signalBus;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        
        private GameProgressReactive _gameProgress;

        public GameProgressReactive GameProgress => _gameProgress;

        public GameProgressService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void MakeTransition(GameProgressTransition transition)
        {
            transition.Execute(_gameProgress);
        }
    }
}
