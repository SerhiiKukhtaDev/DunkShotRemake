using Contexts.Level.Signals;
using Zenject;

namespace Contexts.Project.Installers
{
    public class SignalsInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<StarPickedSignal>();
        }
    }
}
