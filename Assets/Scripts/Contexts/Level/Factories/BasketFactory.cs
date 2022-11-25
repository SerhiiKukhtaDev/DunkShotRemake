using Basket;
using Contexts.Level.Services;
using Core.Factories;
using UnityEngine;
using Utils;
using Zenject;

namespace Contexts.Level.Factories
{
    public interface IBasketFactory : ICustomFactory<BasketBase>
    {
        (BasketBase, BasketBase) CreateInitial(float height);
    }

    public class BasketFactory : IBasketFactory
    {
        private readonly DiContainer _diContainer;
        private readonly BasketBase _prefab;
        private readonly RectTransform[] _spawnAreas;
        private readonly Camera _mainCamera;
        private readonly ScreenScaleNotifier _scaleNotifier;
        private readonly SpawnPoints _spawnPoints;

        private BasketBase _lastCreated;
        private int _lastSpawnAreaIndex;
        private float _height;

        public BasketFactory(DiContainer diContainer, BasketBase prefab, ScreenScaleNotifier scaleNotifier,
            SpawnPoints spawnPoints, Camera mainCamera)
        {
            _spawnPoints = spawnPoints;
            _scaleNotifier = scaleNotifier;
            _diContainer = diContainer;
            _prefab = prefab;
            _spawnAreas = spawnPoints.SpawnAreas;
            _mainCamera = mainCamera;
        }

        public (BasketBase, BasketBase) CreateInitial(float height)
        {
            _height = height;
            var firstBasket = CreateAdaptive();
            firstBasket.transform.position = _spawnPoints.FirstSpawnPoint.position;

            var secondBasket = CreateAdaptive();
            secondBasket.transform.position = _spawnPoints.SecondSpawnPoint.position.AddY(height);
            
            _lastCreated = secondBasket;
            _lastSpawnAreaIndex = 1;

            return (firstBasket, secondBasket);
        }

        public BasketBase Create()
        {
            var basket = CreateAdaptive();
            Vector2 randomPosition = GetRandomPosition();

            basket.transform.SetPosition(randomPosition);

            SetPositionByLastCreated(basket, _height);
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

        private void SetPositionByLastCreated(BasketBase basket, float height)
        {
            var target = basket.transform;
            var position = target.position;
            
            target.SetPosition(new Vector2(position.x, _lastCreated.transform.position.y));
            target.AddYPos(height);
        }

        private BasketBase CreateAdaptive()
        {
            var basket = _diContainer.InstantiatePrefabForComponent<BasketBase>(_prefab.gameObject);
            basket.transform.localScale *= _scaleNotifier.Factor;

            return basket;
        }
    }
}
