using Contexts.Level.Installers;
using Core.StateMachineMediator;

namespace Contexts.Level.StateMachine.States
{
    public class StartLevelState : StateBehaviour
    {
        private readonly LoadLevelMode _loadLevelMode;

        public StartLevelState(LoadLevelMode loadLevelMode)
        {
            _loadLevelMode = loadLevelMode;
        }
        
        protected override void OnEnter()
        {
            if (_loadLevelMode == LoadLevelMode.Reload)
            {
                GoTo<OnLevelReloadedState>();
                return;
            } 
            
            GoTo<OnLevelLoadedWithMenuState>();

        }
    }
}
