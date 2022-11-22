using Basket.Net;
using UnityEngine;

namespace Basket
{
    public class BasketBase : MonoBehaviour
    {
        [SerializeField] private BasketInput input;
        [SerializeField] private BallCatcher catcher;
        [SerializeField] private Transform startPoint;

        public Transform StartPoint => startPoint;

        public BallCatcher Catcher => catcher;

        public BasketInput Input => input;
    }
}
