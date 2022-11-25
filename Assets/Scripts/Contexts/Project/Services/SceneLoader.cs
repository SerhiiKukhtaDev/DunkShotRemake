using System.Threading;
using Contexts.Level.Installers;
using Core.Constants;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace Contexts.Project.Services
{
    public interface ISceneLoader
    {
        UniTask LoadLevel(LoadLevelMode loadMode);
        UniTask Unload(string scene, CancellationToken token = default);
    }

    public class SceneLoader : ISceneLoader
    {
        private readonly ZenjectSceneLoader _zenjectSceneLoader;

        public SceneLoader(ZenjectSceneLoader zenjectSceneLoader)
        {
            _zenjectSceneLoader = zenjectSceneLoader;
        }
        
        public async UniTask LoadLevel(LoadLevelMode loadMode)
        {
            await _zenjectSceneLoader.LoadSceneAsync(Scenes.LevelScene, LoadSceneMode.Single, container =>
            {
                container.BindInstance(loadMode).WhenInjectedInto<LevelInstaller>();
            });
        }
        
        public async UniTask Unload(string scene, CancellationToken token = default)
        {
            await SceneManager.UnloadSceneAsync(scene).ToUniTask(cancellationToken: token);
        }
    }
}
