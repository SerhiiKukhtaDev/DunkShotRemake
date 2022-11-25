using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Contexts.Project.Services
{
    public interface IAssetProvider
    {
        UniTask<T> InstantiateAsync<T>(string key, Transform parent = null) where T : MonoBehaviour;
        void ReleaseObject(GameObject gameObject);
        AsyncOperationHandle<T> LoadAssetWithHandle<T>(string key);
        void Release<T>(T obj);
        AsyncOperationHandle<IList<T>> LoadAssetsAsyncWithHandle<T>(string label, Action<T> callback = null);
        void ReleaseHandle(AsyncOperationHandle handle);
        UniTask<IList<T>> LoadAssetsAsync<T>(string label);
        UniTask<T> LoadComponent<T>(string key, CancellationToken token = default) where T : MonoBehaviour;
        UniTask<GameObject> LoadObject(string key, CancellationToken token = default);
        AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(params string[] keys);
        void ReleaseHandle<TObject>(AsyncOperationHandle<TObject> handle);
    }

    public class AssetProvider : IAssetProvider
    {
        public async UniTask<T> InstantiateAsync<T>(string key, Transform parent = null) where T : MonoBehaviour
        {
            GameObject gameObject = await Addressables.InstantiateAsync(key, parent: parent).Task;

            return gameObject.GetComponent<T>();
        }

        public AsyncOperationHandle<T> LoadAssetWithHandle<T>(string key)
        {
            return Addressables.LoadAssetAsync<T>(key);
        }

        public AsyncOperationHandle<IList<TObject>> LoadAssetsAsync<TObject>(params string[] keys)
        {
            IEnumerable k = keys; 
            return Addressables.LoadAssetsAsync<TObject>(k, null, Addressables.MergeMode.Union);
        }

        public async UniTask<T> LoadComponent<T>(string key, CancellationToken token) where T : MonoBehaviour
        {
            var gameObject = await LoadObject(key, token);
            return gameObject.GetComponent<T>();
        }

        public async UniTask<GameObject> LoadObject(string key, CancellationToken token = default)
        {
            var asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(key);

            var obj =  await asyncOperationHandle.Task;

            obj.OnDestroyAsObservable().Subscribe(_ => ReleaseHandle(asyncOperationHandle));

            return obj;
        }

        public AsyncOperationHandle<IList<T>> LoadAssetsAsyncWithHandle<T>(string label, Action<T> callback = null)
        {
            return Addressables.LoadAssetsAsync(label, callback);
        }
        
        public async UniTask<IList<T>> LoadAssetsAsync<T>(string label)
        {
            return await Addressables.LoadAssetsAsync<T>(label, null);
        }

        public void Release<T>(T obj)
        {
            Addressables.Release(obj);
        }

        public void ReleaseHandle(AsyncOperationHandle handle)
        {
            Addressables.Release(handle);
        }

        public void ReleaseHandle<TObject>(AsyncOperationHandle<TObject> handle)
        {
            Addressables.Release(handle);
        }
        
        public void ReleaseObject(GameObject gameObject)
        {
            Addressables.ReleaseInstance(gameObject);
        }
    }
}
