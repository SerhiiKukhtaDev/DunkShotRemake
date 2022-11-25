using Basket;
using UniRx;
using UnityEngine;
using Utils;
using Views;
using Zenject;

namespace Star
{
    public class StarSpawner : MonoBehaviour
    {
        [SerializeField] private StarCountView starView;
        [SerializeField] private StarBase prefab;
        [SerializeField] private BasketSpawner basketSpawner;
        [SerializeField] private Transform bezierCenterPoint;

        private readonly int[] _randomArr = { 1, 0, 0 };
        
        [Range(0.1f, 1f)] [SerializeField] private float heightOffset = 0.1f;
        private SignalBus _signals;
        private StarCountView _starView;

        [Inject]
        private void Construct(SignalBus signals)
        {
            _starView = starView;
            _signals = signals;
        }
        
        private void Start()
        {
            basketSpawner.NewBasketCreated.Subscribe(TryCreateStar).AddTo(this);
        }

        private void TryCreateStar(BasketBase basket)
        {
            /*int randomValue = _randomArr[Random.Range(0, _randomArr.Length - 1)];

            if (randomValue != 1) return;*/
            
            StarBase star = Instantiate(prefab, basket.transform.position.AddY(heightOffset), Quaternion.identity);
            star.Initialize(bezierCenterPoint, _starView, _signals);
        }
    }
}
