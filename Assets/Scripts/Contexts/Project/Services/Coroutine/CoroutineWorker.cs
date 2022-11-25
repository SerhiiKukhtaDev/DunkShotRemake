using System.Collections;
using UnityEngine;

namespace Contexts.Project.Services.Coroutine
{
    public interface ISimpleCoroutineWorker
    {
        UnityEngine.Coroutine StartCoroutine(IEnumerator coroutine);

        void StopCoroutine(UnityEngine.Coroutine coroutine);
    }
    
    public class CoroutineWorker : MonoBehaviour, ISimpleCoroutineWorker
    {
        
    }
}
