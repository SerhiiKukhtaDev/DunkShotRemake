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
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void Start()
        {
            this.OnTriggerEnter2DAsObservable()
                .FirstOrDefault(col => col.TryGetComponent(out BallFacade _))
                .Subscribe(_ => OnPicked().Forget())
                .AddTo(this);
        }

        public void Initialize(Transform bezierCenterPoint, StarCountView starView)
        {
            _bezierCenterPoint = bezierCenterPoint;
            _startView = starView;
            _target = starView.StarImage.transform;
        }

        private async UniTaskVoid OnPicked()
        {
            _signalBus.Fire<StarPickedSignal>();
            
            var targetPosition = _target.position;
            
            var points = DOCurve.CubicBezier.GetSegmentPointCloud(transform.position, 
                _bezierCenterPoint.position, targetPosition, targetPosition);
            
            await transform.DOPath(points, pathTime).SetEase(Ease.Linear);
            _startView.AnimateAndUpdateCount();

            Destroy(gameObject);
        }
    }
}
