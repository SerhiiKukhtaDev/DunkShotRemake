using UnityEngine;

namespace Ball
{
    public class PredictionBallMovement : BallMovement
    {
        public override void Move(Vector2 force)
        {
            gameObject.SetActive(true);
        
            base.Move(force);
        }

        public void ResetPhysicsAndDisable()
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0;
        
            gameObject.SetActive(false);
        }
    }
}