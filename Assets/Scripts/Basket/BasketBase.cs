using UnityEngine;

namespace Basket
{
    public class BasketBase : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cylinder;

        public float Width => cylinder.bounds.size.x;
    }
}
