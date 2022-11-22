using Basket.Net;
using UniRx;
using UnityEngine;

namespace Basket
{
    public class BasketBase : MonoBehaviour
    {
        [SerializeField] private BasketDeformation deformation;
        [SerializeField] private BallCatcher catcher;
        [SerializeField] private Transform startPoint;

        public Transform StartPoint => startPoint;

        public BallCatcher Catcher => catcher;

        public BasketDeformation Deformation => deformation;

        private void Start()
        {
            catcher.Caught.Subscribe(ball => ball.BallMovement.NotifyInBasket(this)).AddTo(this);
        }
    }
}
