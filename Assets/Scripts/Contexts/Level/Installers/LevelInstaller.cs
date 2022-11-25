using Basket;
using Contexts.Level.Services;
using Contexts.Level.StateMachine;
using Contexts.Project.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Contexts.Level.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private SpawnPoints spawnPoints;

        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasScaler scaler;
        [SerializeField] private BasketInput basketInput;

        [Inject] private LoadLevelMode _loadMode;

        public override void InstallBindings()
        {
            Container.Bind<IWindowsService>().To<WindowsService>().AsSingle();
            Container.Bind<IInputWrapper>().To<InputWrapper>().AsSingle();
            
            Container.BindInterfacesTo<LevelGameProgressSignalsListener>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ScreenScaleNotifier>().AsSingle();
            Container.BindInstance(scaler).WhenInjectedInto<ScreenScaleNotifier>();
            
            Container.BindInterfacesTo<LevelStateMachine>().AsSingle();
            
            Container.BindInstance(basketInput).AsSingle();
            Container.BindInstance(canvas).AsSingle();

            Container.BindInstance(camera).AsSingle();
            Container.BindInstance(spawnPoints).AsSingle();

            Container.BindInstance(_loadMode);
        }
    }

    public enum LoadLevelMode
    {
        Menu,
        Reload
    }
}
