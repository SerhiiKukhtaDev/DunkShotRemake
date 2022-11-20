using UnityEngine;

namespace Basket
{
    public class BasketSpawner : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private BasketBase prefab;

        public BasketBase Spawn()
        {
            return Instantiate(prefab, container);
        }

        public void Destroy(BasketBase basket)
        {
            Object.Destroy(basket);
        }
    }
}
