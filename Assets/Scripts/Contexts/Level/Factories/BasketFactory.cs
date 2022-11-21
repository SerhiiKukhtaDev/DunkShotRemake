using Basket;
using Core.Factories;
using UnityEngine;
using Utils;
using Zenject;

namespace Contexts.Level.Factories
{
    public interface IBasketFactory : ICustomFactory<BasketBase>
    {
        (BasketBase, BasketBase) CreateInitial();
    }

    public class BasketFactory : IBasketFactory
    {
        private readonly DiContainer _diContainer;
        private readonly BasketBase _prefab;
        private readonly float _height;
        private readonly Transform[] _spawnPoints;
        private readonly RectTransform[] _spawnAreas;
        private readonly Camera _mainCamera;
        private readonly ScreenScaleNotifier _scaleNotifier;

        private BasketBase _lastCreated;
        private int _lastSpawnAreaIndex;

        public BasketFactory(DiContainer diContainer, BasketBase prefab, float height, ScreenScaleNotifier scaleNotifier, 
            Transform[] spawnPoints, RectTransform[] spawnAreas, Camera mainCamera)
        {
            _scaleNotifier = scaleNotifier;
            _diContainer = diContainer;
            _prefab = prefab;
            _height = height;
            _spawnPoints = spawnPoints;
            _spawnAreas = spawnAreas;
            _mainCamera = mainCamera;
        }

        public (BasketBase, BasketBase) CreateInitial()
        {
            var firstBasket = CreateAdaptive();
            firstBasket.transform.position = _spawnPoints[0].position;

            var secondBasket = CreateAdaptive();
            secondBasket.transform.position = _spawnPoints[1].position.AddY(_height);
            
            _lastCreated = secondBasket;
            _lastSpawnAreaIndex = 1;

            return (firstBasket, secondBasket);
        }

        public BasketBase Create()
        {
            var basket = CreateAdaptive();
            Vector2 randomPosition = GetRandomPosition();

            basket.transform.SetPosition(randomPosition);

            SetPositionByLastCreated(basket);
            _lastCreated = basket;

            return basket;
        }

        private Vector2 GetRandomPosition()
        {
            if (_lastSpawnAreaIndex == 0)
            {
                var rectLeft = _spawnAreas[1];
                _lastSpawnAreaIndex = 1;

                return _mainCamera.ViewportToWorldPoint(
                    new Vector2(Random.Range(rectLeft.anchorMin.x, rectLeft.anchorMax.x), 0));
            }
            
            var rectRight = _spawnAreas[0];
            _lastSpawnAreaIndex = 0;

            return _mainCamera.ViewportToWorldPoint(
                new Vector2(Random.Range(rectRight.anchorMax.x, rectRight.anchorMin.x), 0));
        }

        private void SetPositionByLastCreated(BasketBase basket)
        {
            var target = basket.transform;
            var position = target.position;
            
            target.SetPosition(new Vector2(position.x, _lastCreated.transform.position.y));
            target.AddYPos(_height);
        }

        private BasketBase CreateAdaptive()
        {
            var basket = _diContainer.InstantiatePrefabForComponent<BasketBase>(_prefab.gameObject);
            basket.transform.localScale *= _scaleNotifier.Factor;

            return basket;
        }
    }
}
