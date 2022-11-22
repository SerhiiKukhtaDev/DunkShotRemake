using UnityEngine;

namespace Ball
{
    public class BallFacade : MonoBehaviour
    {
        [SerializeField] private MainBallMovement ballMovement;
        [SerializeField] private BallPrediction ballPrediction;

        public MainBallMovement BallMovement => ballMovement;

        public BallPrediction BallPrediction => ballPrediction;
    }
}
