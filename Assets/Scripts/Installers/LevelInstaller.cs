using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Camera mainCamera;

        public override void InstallBindings()
        {
            Container.BindInstance(mainCamera).AsSingle();
            Container.Bind<IInputWrapper>().To<InputWrapper>().AsSingle();
        }
    }
}
