using System;
using Ball;
using UniRx;
using UnityEngine;

namespace Basket.Net
{
    public class BallCatcher : MonoBehaviour
    {
        [SerializeField] private new NetCollider collider;
        [SerializeField] private Transform targetPoint;

        private bool _isBallInBasket;
        private IDisposable _fixDisposable;
        
        public IObservable<BallFacade> Caught => _caught;
        private readonly Subject<BallFacade> _caught = new Subject<BallFacade>();

        private void Start()
        {
            collider.TriggerEnter
                .Select(c => c.gameObject)
                .Where(_ => !_isBallInBasket)
                .Subscribe(OnCollision)
                .AddTo(this);
        }

        public void StopCatching(MainBallMovement ballMovement)
        {
            _fixDisposable.Dispose();
            
            ballMovement.StartPhysics();
            _isBallInBasket = false;
        }
        
        private void OnCollision(GameObject obj)
        {
            if (!obj.TryGetComponent(out BallFacade ball)) return;

            _isBallInBasket = true;
            ball.BallMovement.ResetPhysics();

            _fixDisposable = 
                Observable.EveryUpdate().Subscribe(_ => ball.transform.position = targetPoint.position).AddTo(this);

            _caught.OnNext(ball);
        }
    }
}
