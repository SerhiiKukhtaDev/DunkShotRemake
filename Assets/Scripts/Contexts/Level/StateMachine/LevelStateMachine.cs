using Contexts.Level.StateMachine.States;
using Zenject;

namespace Contexts.Level.StateMachine
{
    public class LevelStateMachine : Core.StateMachineMediator.StateMachine
    {
        public LevelStateMachine(DiContainer container) : base(container)
        {
        }

        protected override void Initialize()
        {
            ChangeState<SpawnState>();
        }
    }
}
