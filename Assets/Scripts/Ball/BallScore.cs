using Contexts.Level.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ball
{
    public class BallScore : MonoBehaviour
    {
        [SerializeField] private float comboTime = 5f;

        private bool _comboEnabled;
        private int _enteredBasketCount;
        private float _currentComboTime;
        
        private SignalBus _signals;

        [Inject]
        private void Construct(SignalBus signals)
        {
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

        public void Award()
        {
            _enteredBasketCount++;

            if (_enteredBasketCount > 1)
            {
                _comboEnabled = true;
                _currentComboTime = comboTime;
            }
            
            _signals.Fire(new BallHitTheBasketSignal(_enteredBasketCount));
        }
    }
}
