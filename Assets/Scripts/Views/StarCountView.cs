using System;
using Contexts.Project.Services.Progress;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Views
{
    public class StarCountView : MonoBehaviour
    {
        [SerializeField] private float animationTime = 0.2f;
        [SerializeField] private Image starImage;
        [SerializeField] private TMP_Text starsCount;
        
        private IGameProgressService _progressService;

        [Inject]
        private void Construct(IGameProgressService progressService)
        {
            _progressService = progressService;
        }
        
        private void Start()
        {
            starsCount.text = _progressService.GameProgress.Stars.Value.ToString();
        }

        public Image StarImage => starImage;

        public void AnimateAndUpdateCount()
        {
            starsCount.text = _progressService.GameProgress.Stars.Value.ToString();
            
            starImage.transform.DOScale(starImage.transform.localScale.AddValue(animationTime), 
                animationTime).SetLoops(2, LoopType.Yoyo).SetEase(Ease.Flash).SetAutoKill();
        }
    }
}
