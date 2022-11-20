using UniRx;
using UnityEngine;

namespace Basket
{
    public class BasketSwitcher : MonoBehaviour
    {
        [SerializeField] private BasketSpawner spawner;

        private BasketBase _currentBasket;
        private BasketBase _nextBasket;

        public BasketBase CurrentBasket => _currentBasket;

        private void Start()
        {
            CreateInitial();
        }

        private void CreateInitial()
        {
            /*_currentBasket = spawner.Spawn();
            _nextBasket = CreateNew();*/
        }

        private void OnBallCaught(BasketBase newBasket, Ball ball)
        {
            if (newBasket.Equals(_currentBasket)) return;
            
            //spawner.Destroy(_currentBasket);
            
            /*_currentBasket = newBasket;
            _nextBasket = CreateNew();*/
        }

        private BasketBase CreateNew()
        {
            var basket = spawner.Spawn();
            basket.BallCatcher.Caught.First().Subscribe(ball => OnBallCaught(basket, ball)).AddTo(basket);

            return basket;
        }
    }
}
