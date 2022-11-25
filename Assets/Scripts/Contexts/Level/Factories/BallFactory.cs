using Ball;
using Contexts.Level.Services;
using Core.Factories;
using UnityEngine;
using Zenject;

namespace Contexts.Level.Factories
{
    public interface IBallFactory : ICustomFactory<BallFacade>
    {
        public BallFacade Ball { get; }
    }

    public class BallFactory : IBallFactory
    {
        private readonly DiContainer _diContainer;
        private readonly BallFacade _prefab;
        
        private readonly SpawnPoints _spawnPoints;
        private readonly ScreenScaleNotifier _scaleNotifier;

        public BallFacade Ball { get; private set; }

        public BallFactory(DiContainer diContainer, BallFacade prefab, SpawnPoints spawnPoints, ScreenScaleNotifier scaleNotifier)
        {
            _spawnPoints = spawnPoints;
            _scaleNotifier = scaleNotifier;
            _prefab = prefab;
            _diContainer = diContainer;
        }

        public BallFacade Create()
        {
            Ball = _diContainer.InstantiatePrefabForComponent<BallFacade>(_prefab);
            
            Ball.transform.position = _spawnPoints.BallPosition.position;
            Ball.transform.localScale *= _scaleNotifier.Factor;

            return Ball;
        }
    }
}
