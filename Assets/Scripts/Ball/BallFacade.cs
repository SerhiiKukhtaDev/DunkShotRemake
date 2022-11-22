using UnityEngine;

namespace Ball
{
    public class BallFacade : MonoBehaviour
    {
        [SerializeField] private MainBallMovement ballMovement;
        [SerializeField] private BallPrediction ballPrediction;
        [SerializeField] private BallScore ballScore;
        
        public MainBallMovement BallMovement => ballMovement;

        public BallPrediction BallPrediction => ballPrediction;

        public BallScore BallScore => ballScore;
    }
}
