using System.Collections.Generic;
using Contexts.Project.Services;
using Contexts.Project.Services.Coroutine;
using Contexts.Project.Services.Progress;
using ScriptableObjects.Settings.Base;
using UnityEngine;
using Zenject;

namespace Contexts.Project.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineWorker coroutineWorker;
        [SerializeField] private Canvas parent;
        [SerializeField] private List<SettingBase> settings;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<SettingsService>().AsSingle();
            Container.BindInstance(settings).WhenInjectedInto<ISettingsService>();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.BindInterfacesTo<GameProgressService>().AsSingle();
            Container.Bind<IWindowsService>().To<WindowsService>().FromSubContainerResolve()
                .ByMethod(InstallWindows).WithKernel().AsSingle();

            Container.Bind<ISimpleCoroutineWorker>().FromInstance(coroutineWorker).AsSingle();
        }

        private void InstallWindows(DiContainer container)
        {
            container.Bind<WindowsService>().AsSingle();
            container.BindInstance(parent.transform).AsSingle();
        }
    }
}
