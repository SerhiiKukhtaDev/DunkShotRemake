using Ball;
using Contexts.Level.Signals;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Views;
using Zenject;

namespace Star
{
    public class StarBase : MonoBehaviour
    {
        [SerializeField] private float pathTime = 1f;
        
        private StarCountView _startView;
        private Transform _target;
        private Transform _bezierCenterPoint;
        private SignalBus _signals;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        private void Start()
        {
            this.OnTriggerEnter2DAsObservable()
                .First(col => col.TryGetComponent(out BallFacade _))
                .Subscribe(_ => OnPicked().Forget())
                .AddTo(_disposable);
        }

        public void Initialize(Transform bezierCenterPoint, StarCountView starView, SignalBus signals)
        {
            _signals = signals;
            _bezierCenterPoint = bezierCenterPoint;
            _startView = starView;
            _target = starView.StarImage.transform;
        }

        private async UniTaskVoid OnPicked()
        {
            _signals.Fire<StarPickedSignal>();
            
            var targetPosition = _target.position;
            
            var points = DOCurve.CubicBezier.GetSegmentPointCloud(transform.position, 
                _bezierCenterPoint.position, targetPosition, targetPosition);
            
            await transform.DOPath(points, pathTime).SetEase(Ease.Linear);
            _startView.AnimateAndUpdateCount();

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (_disposable.Count > 0 && !_disposable.IsDisposed)
                _disposable.Dispose();
        }
    }
}
