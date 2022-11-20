using UnityEngine;

public abstract class BallMovement : MonoBehaviour
{
    [SerializeField] protected new Rigidbody2D rigidbody;

    public virtual void Move(Vector2 startMousePosition, Vector2 endMousePosition, float force)
    {
        rigidbody.rotation = 100;
        rigidbody.AddForce((startMousePosition - endMousePosition) * force, ForceMode2D.Force);
    }

    public class MainBallMovement : BallMovement
    {
        public void ResetPhysics()
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0;
            rigidbody.rotation = 0;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
