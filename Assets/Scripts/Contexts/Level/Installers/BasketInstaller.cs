using Basket;
using Contexts.Level.Factories;
using UnityEngine;
using Zenject;

namespace Contexts.Installers
{
    public class BasketInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform[] spawnAreas = new RectTransform[2];
        [SerializeField] private BasketBase prefab;
        [SerializeField] private float height;

        public override void InstallBindings()
        {
            Container
                .Bind<IBasketFactory>()
                .To<BasketFactory>()
                .FromSubContainerResolve()
                .ByMethod(BindFactory)
                .AsSingle();
        }

        private void BindFactory(DiContainer container)
        {
            container.Bind<BasketFactory>().AsSingle();
            container.BindInstance(height);
            container.BindInstance(prefab);
            container.BindInstance(spawnAreas);
        }
    }
}
