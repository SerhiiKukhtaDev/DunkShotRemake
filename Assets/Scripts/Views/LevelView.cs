using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public interface ILevelView
    {
        void Show();
    }

    [RequireComponent(typeof(LevelView))]
    public class LevelView : MonoBehaviour, ILevelView
    {
        [SerializeField] private float animationTime = 0.5f;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private GraphicRaycaster raycaster;
        
        public void Show()
        {
            raycaster.enabled = true;
            group.DOFade(1f, animationTime).SetAutoKill();
        }
    }
}