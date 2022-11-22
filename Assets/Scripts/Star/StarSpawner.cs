using Basket;
using UniRx;
using UnityEngine;
using Utils;

namespace Star
{
    public class StarSpawner : MonoBehaviour
    {
        [SerializeField] private StarBase prefab;
        [SerializeField] private BasketSpawner basketSpawner;
        [Range(0.1f, 1f)] [SerializeField] private float heightOffset = 0.1f;
        
        private void Start()
        {
            basketSpawner.NewBasketCreated.Subscribe(TryCreateStar).AddTo(this);
        }

        private void TryCreateStar(BasketBase basket)
        {
            Instantiate(prefab, basket.transform.position.AddY(heightOffset), Quaternion.identity);
        }
    }
}
