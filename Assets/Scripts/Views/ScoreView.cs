using Contexts.Project.Services.Progress;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Views
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        
        private IGameProgressService _progressService;

        [Inject]
        private void Construct(IGameProgressService progressService)
        {
            _progressService = progressService;
        }

        private void Start()
        {
            _progressService.GameProgress.Score.Subscribe(score => scoreText.text = score.ToString()).AddTo(this);
        }
    }
}
