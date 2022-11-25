using System;
using Basket;
using Contexts.Level.Services.Audio;
using Contexts.Level.Signals;
using UniRx;
using UnityEngine;
using Zenject;
using AudioType = ScriptableObjects.Audios.AudioType;

namespace Ball
{
    public class MainBallMovement : BallMovement
    {
        public IObservable<Vector2> Moved => _moved;
        private readonly Subject<Vector2> _moved = new Subject<Vector2>();

        public IObservable<BasketBase> InBasket => _inBasket;
        private readonly ReplaySubject<BasketBase> _inBasket = new ReplaySubject<BasketBase>(1);
        
        private SignalBus _signalBus;
        private Vector2 _savedVelocity;
        private float _savedAngularVelocity;
        private IAudioService _audioService;

        [Inject]
        private void Construct(SignalBus signalBus, IAudioService audioService)
        {
            _audioService = audioService;
            _signalBus = signalBus;
        }
        
        private void Start()
        {
            _signalBus.GetStream<GamePausedSignal>().Subscribe(_ => OnPause()).AddTo(this);
            _signalBus.GetStream<GameResumedSignal>().Subscribe(_ => OnResume()).AddTo(this);
        }

        private void OnPause()
        {
            _savedVelocity = rigidbody.velocity;
            _savedAngularVelocity = rigidbody.angularVelocity;
            rigidbody.bodyType = RigidbodyType2D.Static;
        }
        
        private void OnResume() 
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            rigidbody.velocity = _savedVelocity;
            rigidbody.angularVelocity = _savedAngularVelocity;
        }

        public override void Move(Vector2 force)
        {
            _moved.OnNext(force);
            _audioService.Play(AudioType.BallFlight);
            
            base.Move(force);
        }

        public void ResetPhysics()
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0;
            rigidbody.rotation = 0;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void StartPhysics()
        {
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }

        public void NotifyInBasket(BasketBase basket)
        {
            _inBasket.OnNext(basket);
        }
    }
}