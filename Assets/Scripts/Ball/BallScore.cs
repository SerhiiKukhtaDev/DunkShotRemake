using Basket;
using Contexts.Level.Services.Audio;
using Contexts.Level.Signals;
using UnityEngine;
using Zenject;
using AudioType = ScriptableObjects.Audios.AudioType;

namespace Ball
{
    public class BallScore : MonoBehaviour
    {
        [SerializeField] private float comboTime = 5f;

        private bool _comboEnabled;
        private int _enteredBasketCount;
        private float _currentComboTime;
        
        private SignalBus _signals;
        private IAudioService _audioService;

        [Inject]
        private void Construct(SignalBus signals, IAudioService audioService)
        {
            _audioService = audioService;
            _signals = signals;
        }
        
        private void Update()
        {
            if (_comboEnabled)
            {
                _currentComboTime -= Time.deltaTime;
            }

            if (_comboEnabled && _currentComboTime <= 0)
            {
                _comboEnabled = false;
                _enteredBasketCount = 0;
            }
        }

        public void Award(BasketBonusChecker basket)
        {
            _enteredBasketCount++;

            if (_enteredBasketCount > 1)
            {
                _comboEnabled = true;
                _currentComboTime = comboTime;
            }
            
            _audioService.Play(_enteredBasketCount > 1 ? AudioType.PerfectHit : AudioType.NotPerfectHit);
            
            _signals.Fire(new BallHitTheBasketSignal(_enteredBasketCount, basket));
        }
    }
}
