using Contexts.Project.Services.Progress;
using Core.StateMachineMediator;
using UniRx;

namespace Contexts.Boot.StateMachine.States
{
    public class LoadPlayerProgressState : StateBehaviour
    {
        private readonly IGameProgressLoader _loader;

        public LoadPlayerProgressState(IGameProgressLoader loader)
        {
            _loader = loader;
        }
        
        protected override void OnEnter()
        {
            _loader.Load();
            
            GoTo<LoadLevel>();
        }
    }
}
