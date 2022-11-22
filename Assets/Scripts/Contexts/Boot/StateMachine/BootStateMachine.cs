using Contexts.Boot.StateMachine.States;
using Zenject;

namespace Contexts.Boot.StateMachine
{
    public class BootStateMachine : Core.StateMachineMediator.StateMachine
    {
        public BootStateMachine(DiContainer container) : base(container)
        {
        }

        protected override void Initialize()
        {
            ChangeState<PlayStartCompanyAnimationState>();
        }
    }
}
