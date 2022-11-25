using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public interface IMenuView
    {
        UniTaskVoid Show();
        
        UniTask Hide();
        void HideInstant();
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class MenuView : MonoBehaviour, IMenuView
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float animationTime = 0.5f;
        [SerializeField] private GraphicRaycaster raycaster;

        public async UniTaskVoid Show()
        {
            await canvasGroup.DOFade(1f, animationTime).WithCancellation(this.GetCancellationTokenOnDestroy());
            canvasGroup.interactable = true;
            raycaster.enabled = true;
        }

        public async UniTask Hide()
        {
            raycaster.enabled = false;
            canvasGroup.interactable = false;
            await canvasGroup.DOFade(0f, animationTime).WithCancellation(this.GetCancellationTokenOnDestroy());
        }

        public void HideInstant()
        {
            raycaster.enabled = false;
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0;
        }
    }
}
