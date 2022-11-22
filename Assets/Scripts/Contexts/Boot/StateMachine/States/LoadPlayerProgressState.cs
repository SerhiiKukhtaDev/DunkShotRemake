using Core.StateMachineMediator;

namespace Contexts.Boot.StateMachine.States
{
    public class LoadPlayerProgressState : StateBehaviour
    {
        protected override void OnEnter()
        {
            GoTo<LoadMenuState>();
        }
    }
}