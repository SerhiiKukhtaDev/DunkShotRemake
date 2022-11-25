using Contexts.Project.Services;
using UnityEngine;
using Zenject;

namespace Contexts.Menu.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private Canvas parent;

        public override void InstallBindings()
        {
            Container.BindInstance(parent).AsSingle();
            Container.Bind<IWindowsService>().To<WindowsService>().AsSingle();
        }
    }
}
