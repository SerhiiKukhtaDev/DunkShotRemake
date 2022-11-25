using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] protected float showAnimationTime = 0.5f;
        [SerializeField] protected float hideAnimationTime = 0.5f;
        [SerializeField] protected RectTransform panel;
        [SerializeField] protected CanvasGroup canvasGroup;

        public void SetInteractable(bool interactable)
        {
            canvasGroup.interactable = interactable;
        }
        
        public virtual async UniTask Show()
        {
            var ct = this.GetCancellationTokenOnDestroy();
            
            panel.localScale = Vector3.zero;
            canvasGroup.alpha = 0;

            await UniTask.WhenAll(canvasGroup.DOFade(1, showAnimationTime).WithCancellation(ct),
                panel.DOScale(1, showAnimationTime).WithCancellation(ct));
        }

        public virtual async UniTask Hide()
        {
            var ct = this.GetCancellationTokenOnDestroy();

            await UniTask.WhenAll(canvasGroup.DOFade(0, showAnimationTime).WithCancellation(ct),
                panel.DOScale(0, showAnimationTime).WithCancellation(ct));
            
            Destroy(gameObject);
        }
    }
}