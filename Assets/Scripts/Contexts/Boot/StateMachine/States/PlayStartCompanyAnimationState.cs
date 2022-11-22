using Core.StateMachineMediator;

namespace Contexts.Boot.StateMachine.States
{
    public class PlayStartCompanyAnimationState : StateBehaviour
    {
        protected override void OnEnter()
        {
            GoTo<LoadSettingsState>();
        }
    }
}
