using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Contexts.Project.Services
{
    public interface ISceneLoader
    {
        UniTask Load(string scene, CancellationToken token);
        UniTask LoadAdditive(string scene, CancellationToken token);
        UniTask Unload(string scene, CancellationToken token);
    }

    public class SceneLoader : ISceneLoader
    {
        public async UniTask Load(string scene, CancellationToken token)
        {
            await Load(scene, LoadSceneMode.Single, token);
        }

        public async UniTask LoadAdditive(string scene, CancellationToken token)
        {
            await Load(scene, LoadSceneMode.Additive, token);
        }

        public async UniTask Unload(string scene, CancellationToken token)
        {
            await SceneManager.UnloadSceneAsync(scene).ToUniTask(cancellationToken: token);
        }

        private static async UniTask Load(string scene, LoadSceneMode mode, CancellationToken token = default)
        {
            var ao = SceneManager.LoadSceneAsync(scene, mode);

            await ao.ToUniTask(cancellationToken: token);
        }
    }
}
