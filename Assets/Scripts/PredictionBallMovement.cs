using UnityEngine;

public class PredictionBallMovement : BallMovement
{
    public override void Move(Vector2 startMousePosition, Vector2 endMousePosition, float force)
    {
        gameObject.SetActive(true);
        
        base.Move(startMousePosition, endMousePosition, force);
    }

    public void ResetPhysicsAndDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        
        gameObject.SetActive(false);
    }
}