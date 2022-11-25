using Contexts.Project.Services;
using UnityEngine;
using Zenject;

namespace Contexts.Level.Installers
{
    public class LevelViewInstaller : MonoInstaller
    {
        [SerializeField] private Canvas levelCanvas;

        public override void InstallBindings()
        {
            Container.Bind<IWindowsService>().To<WindowsService>().AsSingle();
            Container.BindInstance(levelCanvas).AsSingle();
        }
    }
}