using Core.Factories;
using UnityEngine;
using Zenject;

namespace Contexts.Level
{
    public class BallFactory : ICustomFactory<Vector2, Ball>
    {
        private readonly DiContainer _diContainer;
        private readonly Ball _prefab;

        public BallFactory(DiContainer diContainer, Ball prefab)
        {
            _prefab = prefab;
            _diContainer = diContainer;
        }
        
        public Ball Create(Vector2 position)
        {
            Ball ball = _diContainer.InstantiatePrefabForComponent<Ball>(_prefab);
            ball.transform.position = position;

            return ball;
        }
    }
}
