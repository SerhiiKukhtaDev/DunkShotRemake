using Ball;
using Core.Factories;
using UnityEngine;
using Zenject;

namespace Contexts.Level
{
    public class BallFactory : ICustomFactory<Vector2, BallFacade>
    {
        private readonly DiContainer _diContainer;
        private readonly BallFacade _prefab;

        public BallFactory(DiContainer diContainer, BallFacade prefab)
        {
            _prefab = prefab;
            _diContainer = diContainer;
        }
        
        public BallFacade Create(Vector2 position)
        {
            BallFacade ball = _diContainer.InstantiatePrefabForComponent<BallFacade>(_prefab);
            ball.transform.position = position;

            return ball;
        }
    }
}
