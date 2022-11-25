using Ball;
using Contexts.Level.Factories;
using UnityEngine;
using Zenject;

namespace Contexts.Level.Installers
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private BallFacade ball;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IBallFactory>()
                .To<BallFactory>()
                .FromSubContainerResolve()
                .ByMethod(BindFactory)
                .AsSingle();
        }

        private void BindFactory(DiContainer container)
        {
            container.Bind<BallFactory>().AsSingle();
            container.BindInstance(ball);
        }
    }
}
