using Basket.Net;
using UnityEngine;

namespace Basket
{
    public class BasketBase : MonoBehaviour
    {
        [SerializeField] private BallCatcher ballCatcher;

        public BallCatcher BallCatcher => ballCatcher;
    }
}
