using Contexts.Level.Signals;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Contexts.Level.Services
{
    public class ScoreComboNotifier : MonoBehaviour
    {
        [SerializeField] private Text comboText;
        [SerializeField] private Text statusText;
        
        [SerializeField] private float appearHeightOffset = 0.3f;
        [SerializeField] private float textScale = 1.5f;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            _signalBus.GetStream<BallHitTheBasketSignal>().Subscribe(s => OnBallHit(s).Forget()).AddTo(this);
        }

        private async UniTaskVoid OnBallHit(BallHitTheBasketSignal signal)
        {
            if (signal.Combo == 1) return;
            
            statusText.transform.localScale = SetScale(textScale);
            AnimateText(signal, statusText);
            
            await UniTask.Delay(100);
            
            if (signal.Combo < 2) return;

            comboText.transform.localScale = SetScale(textScale);

            comboText.text = $"+{signal.Combo}";
            AnimateText(signal, comboText, 0.6f, 1.5f);
        }

        private void AnimateText(BallHitTheBasketSignal signal, Text text, float height = 0.4f, float duration = 1f)
        {
            text.transform.position = signal.Basket.transform.position.AddY(appearHeightOffset);

            text.transform.DOMoveY(text.transform.position.y + height, duration);
            text.transform.DOScale(Vector3.zero, duration);
        }

        private Vector3 SetScale(float value)
        {
            return new Vector3(value, value, value);
        }
    }
}
