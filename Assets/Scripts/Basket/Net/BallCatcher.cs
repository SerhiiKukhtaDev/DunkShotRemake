using System;
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
        
        public IObservable<MainBallMovement> Caught => _caught;
        private readonly Subject<MainBallMovement> _caught = new Subject<MainBallMovement>();

        public IObservable<Unit> Left => _left;
        private readonly Subject<Unit> _left = new Subject<Unit>();

        private void Start()
        {
            collider.TriggerEnter
                .Select(c => c.gameObject)
                .Where(_ => !_isBallInBasket)
                .Subscribe(OnCollision)
                .AddTo(this);

            collider.TriggerExit.Where(col => col.TryGetComponent(out MainBallMovement movement))
                .Subscribe(_ => _left.OnNext(Unit.Default)).AddTo(this);
        }

        public void StopCatching(MainBallMovement ballMovement)
        {
            _fixDisposable.Dispose();
            
            ballMovement.StartPhysics();
            _isBallInBasket = false;
        }

        //todo move to serializeField
        private async void OnCollision(GameObject obj)
        {
            if (!obj.TryGetComponent(out MainBallMovement ball)) return;

            _isBallInBasket = true;
            ball.ResetPhysics();

            _fixDisposable = 
                Observable.EveryUpdate().Subscribe(_ => ball.transform.position = targetPoint.position);

            _caught.OnNext(ball);
        }
    }
}
