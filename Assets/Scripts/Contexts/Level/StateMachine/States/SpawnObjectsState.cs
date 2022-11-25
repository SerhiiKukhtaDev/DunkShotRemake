using Ball;
using Basket;
using Contexts.Level.Factories;
using Core.Factories;
using Core.StateMachineMediator;

namespace Contexts.Level.StateMachine.States
{
    public class SpawnState : StateBehaviour
    {
        private readonly IBasketSpawner _spawner;
        private readonly ICustomFactory<BallFacade> _ballFactory;

        public SpawnState(IBasketSpawner spawner, IBallFactory ballFactory)
        {
            _spawner = spawner;
            _ballFactory = ballFactory;
        }
        
        protected override void OnEnter()
        {
            _spawner.CreateInitial();
            _ballFactory.Create();
            
            GoTo<StartLevelState>();
        }
    }
}
