using Contexts.Boot.StateMachine;
using Zenject;

namespace Contexts.Boot.Installers
{
    public class BootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootStateMachine>().AsSingle();
        }
    }
}
