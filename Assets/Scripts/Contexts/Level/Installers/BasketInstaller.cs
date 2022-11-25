using Basket;
using Contexts.Level.Factories;
using UnityEngine;
using Zenject;

namespace Contexts.Level.Installers
{
    public class BasketInstaller : MonoInstaller
    {
        [SerializeField] private BasketSpawner spawner;
        [SerializeField] private BasketBase prefab;

        public override void InstallBindings()
        {
            Container
                .Bind<IBasketFactory>()
                .To<BasketFactory>()
                .FromSubContainerResolve()
                .ByMethod(BindFactory)
                .AsSingle();

            Container.Bind<IBasketSpawner>().FromInstance(spawner).AsSingle();
        }

        private void BindFactory(DiContainer container)
        {
            container.Bind<BasketFactory>().AsSingle();
            container.BindInstance(prefab);
        }
    }
}
