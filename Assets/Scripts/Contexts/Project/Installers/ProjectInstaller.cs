using System.Collections.Generic;
using Contexts.Project.Services;
using Contexts.Project.Services.Progress;
using ScriptableObjects.Settings.Base;
using UnityEngine;
using Zenject;

namespace Contexts.Project.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private List<SettingBase> settings;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<SettingsService>().AsSingle();
            Container.BindInstance(settings).WhenInjectedInto<ISettingsService>();
            Container.BindInterfacesTo<GameProgressService>().AsSingle();
        }
    }
}
