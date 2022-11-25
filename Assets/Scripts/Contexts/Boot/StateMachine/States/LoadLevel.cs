using Contexts.Level.Installers;
using Contexts.Project.Services;
using Core.StateMachineMediator;

namespace Contexts.Boot.StateMachine.States
{
    public class LoadLevel : StateBehaviour
    {
        private readonly ISceneLoader _sceneLoader;

        public LoadLevel(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        protected override async void OnEnter()
        {
            await _sceneLoader.LoadLevel(LoadLevelMode.Menu);
        }
    }
}
