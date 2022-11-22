using Contexts.Project.Services;
using Core.Constants;
using Core.StateMachineMediator;

namespace Contexts.Boot.StateMachine.States
{
    public class LoadMenuState : StateBehaviour
    {
        private readonly ISceneLoader _sceneLoader;

        public LoadMenuState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        protected override async void OnEnter()
        {
            await _sceneLoader.Load(Scenes.LevelScene, default);
        }
    }
}