using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Contexts.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CanvasScaler scaler;
        
        public override void InstallBindings()
        {
            Container.BindInstance(mainCamera).AsSingle();
            Container.Bind<IInputWrapper>().To<InputWrapper>().AsSingle();

            Container.BindInterfacesAndSelfTo<ScreenScaleNotifier>().AsSingle();
            Container.BindInstance(scaler).WhenInjectedInto<ScreenScaleNotifier>();
        }
    }
}
