using System;
using Contexts.Level.Factories;
using UniRx;
using UnityEngine;
using Zenject;

namespace Basket
{
    public interface IBasketSpawner
    {
        IObservable<BasketBase> NewBasketCreated { get; }
        BasketBase CurrentBasket { get; }
        BasketBase NextBasket { get; }
        void CreateInitial();
    }

    public class BasketSpawner : MonoBehaviour, IBasketSpawner
    {
        [SerializeField] private float height = 2.5f;
        
        public IObservable<BasketBase> NewBasketCreated => _newBasketCreated;
        private readonly Subject<BasketBase> _newBasketCreated = new Subject<BasketBase>();

        public float Height => height;

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

        public void CreateInitial()
        {
            (_currentBasket, _nextBasket) = _basketFactory.CreateInitial(height);
            _nextBasket.Catcher.Caught.First().Subscribe(_ => CreateNext()).AddTo(_disposable);
        }

        private void CreateNext()
        {
            Destroy(_currentBasket.gameObject);
            
            _currentBasket = _nextBasket;
            _nextBasket = _basketFactory.Create();
            _nextBasket.Catcher.Caught.First().Subscribe(_ => CreateNext()).AddTo(_disposable);
            _newBasketCreated.OnNext(_nextBasket);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}
