using Contexts.Level.Factories;
using UniRx;
using UnityEngine;
using Zenject;

namespace Basket
{
    public class BasketSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] startPoints = new Transform[2];
        
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        
        private IBasketFactory _basketFactory;

        private BasketBase _currentBasket;
        private BasketBase _nextBasket;

        public BasketBase CurrentBasket => _currentBasket;

        public BasketBase NextBasket => _nextBasket;

        [Inject]
        private void Construct(IBasketFactory basketFactory)
        {
            _basketFactory = basketFactory;
        }

        private void Start()
        {
            (_currentBasket, _nextBasket) = _basketFactory.CreateInitial(startPoints);
            _nextBasket.Catcher.Caught.First().Subscribe(_ => CreateNext()).AddTo(_disposable);
        }

        private void CreateNext()
        {
            Destroy(_currentBasket.gameObject);
            
            _currentBasket = _nextBasket;
            _nextBasket = _basketFactory.Create();
            _nextBasket.Catcher.Caught.First().Subscribe(_ => CreateNext()).AddTo(_disposable);
        }
    }
}
