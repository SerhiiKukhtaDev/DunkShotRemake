using Core;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Contexts.Project.Services
{
    public interface IWindowsService
    {
        UniTask<TWindow> CreateSingle<TWindow>() where TWindow : Window;
    }

    public class WindowsService : IWindowsService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly Canvas _canvas;
        private readonly DiContainer _container;

        public WindowsService(IAssetProvider assetProvider, Canvas canvas, DiContainer container)
        {
            _container = container;
            _assetProvider = assetProvider;
            _canvas = canvas;
        }

        public async UniTask<TWindow> CreateSingle<TWindow>() where TWindow : Window
        {
            return await CreateSingleInternal<TWindow>();
        }

        private async UniTask<TWindow> CreateSingleInternal<TWindow>() where TWindow : MonoBehaviour
        {
            var windowPrefab = await _assetProvider.LoadObject(typeof(TWindow).Name);
            var window = _container.InstantiatePrefabForComponent<TWindow>(windowPrefab.gameObject, _canvas.transform);

            return window;
        }
    }
}
