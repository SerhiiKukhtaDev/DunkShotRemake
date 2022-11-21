using UnityEngine;

public class MainBallMovement : BallMovement
{
    public void ResetPhysics()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        rigidbody.rotation = 0;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void StartPhysics()
    {
        rigidbody.constraints = RigidbodyConstraints2D.None;
    }
}