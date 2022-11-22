using System.Threading;
using Basket.Net;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Basket
{
    public class BasketDeformation : MonoBehaviour
    {
        [Range(1, 15)] [SerializeField] private float rotateSpeed = 10f;
        [SerializeField] private float catchAnimationDuration = 0.15f;
        [SerializeField] private float catchTension = 1.2f;
        [SerializeField] private float minTension = 1f;
        [SerializeField] private float maxTension = 1.6f;
        [SerializeField] private Transform basket;
        [SerializeField] private Transform net;
        [SerializeField] private BallCatcher catcher;

        private void Start()
        {
            catcher.Caught.Subscribe(_ => AnimateCatch().Forget()).AddTo(this);
        }

        private async UniTaskVoid AnimateCatch()
        {
            var ct = this.GetCancellationTokenOnDestroy();
            
            await AnimateNet(catchTension, catchAnimationDuration, ct);
            await AnimateNet(minTension, catchAnimationDuration, ct);
        }

        public void Deform(Vector2 forceVector, float forceDelta)
        {
            net.localScale = new Vector3(net.localScale.x, Mathf.Lerp(minTension, maxTension, forceDelta));
            
            forceVector.Normalize();
            
            var toRotation = Quaternion.LookRotation(Vector3.forward, forceVector);
            basket.rotation = Quaternion.RotateTowards(basket.rotation, toRotation, 
                rotateSpeed * 100 * Time.deltaTime);
        }

        public async UniTaskVoid AnimateShootAndReset()
        {
            var ct = this.GetCancellationTokenOnDestroy();
            
            await AnimateNet(minTension, catchAnimationDuration, ct);
            await basket.DORotate(Vector3.zero, 0.5f).SetEase(Ease.Linear).SetAutoKill().WithCancellation(ct);
        }

        private async UniTask AnimateNet(float endValue, float time, CancellationToken ct, Ease ease = Ease.Flash)
        {
            await net.DOScaleY(endValue, time).SetEase(ease).SetAutoKill().WithCancellation(ct);
        }
    }
}