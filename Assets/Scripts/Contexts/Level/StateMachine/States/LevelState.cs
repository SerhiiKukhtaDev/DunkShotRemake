using Windows.Lose;
using Contexts.Level.Installers;
using Contexts.Level.Services.Audio;
using Contexts.Level.Signals;
using Contexts.Project.Services;
using Core.StateMachineMediator;
using Cysharp.Threading.Tasks;
using ScriptableObjects.Audios;
using UniRx;
using Views;
using Zenject;
using CompositeDisposable = UniRx.CompositeDisposable;

namespace Contexts.Level.StateMachine.States
{
    public class LevelState : StateBehaviour
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly SignalBus _signalBus;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly IWindowsService _windowsService;
        private IAudioService _audioService;

        public LevelState(ISceneLoader sceneLoader, SignalBus signalBus, IWindowsService windowsService, IAudioService audioService)
        {
            _audioService = audioService;
            _windowsService = windowsService;
            _signalBus = signalBus;
            _sceneLoader = sceneLoader;
        }

        protected override void OnEnter()
        {
            _signalBus.GetStream<GameLoseSignal>().First().Subscribe(_ => OnLose().Forget()).AddTo(_disposable);
            _signalBus.GetStream<LoadMenuSignal>().First().Subscribe(_ => ReloadLevel(LoadLevelMode.Menu)).AddTo(_disposable);
            _signalBus.GetStream<ReloadLevelSignal>().First().Subscribe(_ => ReloadLevel(LoadLevelMode.Reload)).AddTo(_disposable);
        }

        protected override void OnExit()
        {
            if (_disposable.Count > 0)
                _disposable.Dispose();
        }

        private async UniTaskVoid OnLose()
        {
            _audioService.Play(AudioType.Lose);
            var window = await _windowsService.CreateSingle<LoseWindow>();
            window.Show().Forget();
        }

        private void ReloadLevel(LoadLevelMode mode)
        {
            _sceneLoader.LoadLevel(mode).Forget();
        }
    }
}
