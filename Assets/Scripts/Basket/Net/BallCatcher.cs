using System;
using System.Collections;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Basket.Net
{
    public class BallCatcher : MonoBehaviour
    {
        [SerializeField] private Transform net;
        [SerializeField] private NetCollider collider;
        [SerializeField] private Transform targetPoint;
        
        
        public IObservable<Ball> Caught => _caught;
        private readonly Subject<Ball> _caught = new Subject<Ball>();

        private void Start()
        {
            collider.TriggerEnter
                .Select(c => c.gameObject)
                .Subscribe(OnCollision)
                .AddTo(this);
        }

        private void OnCollision(GameObject obj)
        {
            if (obj.TryGetComponent(out BallMovement.MainBallMovement ball))
            {
                net.DOScaleY(1.2f, 0.15f).SetEase(Ease.Flash).OnComplete(() =>
                {
                    net.DOScaleY(1f, 0.15f).SetEase(Ease.Flash);
                });
                
                ball.ResetPhysics();

                /*StartCoroutine(FixBallToPoint(ball, targetPoint, rigidbody2D));

                _caught.OnNext(ball);*/
            }
        }

        private IEnumerator FixBallToPoint(Ball ball, Transform point, Rigidbody2D rigidbody)
        {
            while (rigidbody.velocity.y < 10)
            {
                ball.transform.position = point.position;
                yield return null;
            }
            
            rigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }
}
