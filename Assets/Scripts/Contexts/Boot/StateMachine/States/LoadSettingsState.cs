using Contexts.Project.Services;
using Core.StateMachineMediator;

namespace Contexts.Boot.StateMachine.States
{
    public class LoadSettingsState : StateBehaviour
    {
        private ISettingsService _settingsService;

        public LoadSettingsState(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }
        
        protected override void OnEnter()
        {
            ((SettingsService)_settingsService).LoadSettings();
            GoTo<LoadPlayerProgressState>();
        }
    }
}
