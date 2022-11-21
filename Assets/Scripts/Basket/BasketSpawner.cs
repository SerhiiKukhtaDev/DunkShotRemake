using Contexts.Level.Factories;
using UnityEngine;
using Zenject;

namespace Basket
{
    public class BasketSpawner : MonoBehaviour
    {
        private IBasketFactory _basketFactory;

        [Inject]
        private void Construct(IBasketFactory basketFactory)
        {
            _basketFactory = basketFactory;
        }
        
        private void Start()
        {
            var (firstBasket, secondBasket) = _basketFactory.CreateInitial();

            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
            _basketFactory.Create();
        }
    }
}
