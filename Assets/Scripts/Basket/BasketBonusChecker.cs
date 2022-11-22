using Ball;
using Basket.Net;
using UniRx;
using UnityEngine;

namespace Basket
{
    public class BasketBonusChecker : MonoBehaviour
    {
        [SerializeField] private BallCatcher catcher;

        private void Start()
        {
            catcher.Caught.FirstOrDefault().Subscribe(OnCaught).AddTo(this);
        }

        private void OnCaught(BallFacade ball)
        {
            ball.BallScore.Award(this);
        }
    }
}
